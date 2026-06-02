using Microsoft.Extensions.Logging;
using ReceptionistChatBot.Application.Common.Exceptions;
using ReceptionistChatBot.Application.DTOs.Chat;
using ReceptionistChatBot.Application.DTOs.Prompts;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Application.Interfaces.Services;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Infrastructure.Services;

public sealed class ChatService : IChatService
{
    private const int MaxPromptFaqs = 8;
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly ICompanyInformationRepository _companyInformationRepository;
    private readonly IFaqRepository _faqRepository;
    private readonly IPromptBuilderService _promptBuilderService;
    private readonly IGeminiService _geminiService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChatService> _logger;

    public ChatService(
        IChatSessionRepository chatSessionRepository,
        IMessageRepository messageRepository,
        ICompanyInformationRepository companyInformationRepository,
        IFaqRepository faqRepository,
        IPromptBuilderService promptBuilderService,
        IGeminiService geminiService,
        IUnitOfWork unitOfWork,
        ILogger<ChatService> logger)
    {
        _chatSessionRepository = chatSessionRepository;
        _messageRepository = messageRepository;
        _companyInformationRepository = companyInformationRepository;
        _faqRepository = faqRepository;
        _promptBuilderService = promptBuilderService;
        _geminiService = geminiService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> CreateChatSessionAsync(CancellationToken cancellationToken = default)
    {
        var chatSession = new ChatSession
        {
            Id = Guid.NewGuid(),
            Status = ChatSessionStatus.Active,
            Channel = "Web"
        };

        await _chatSessionRepository.AddAsync(chatSession, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created chat session {ChatSessionId}.", chatSession.Id);

        return chatSession.Id;
    }

    public async Task<ChatResponseDto> SendMessageAsync(
        ChatRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Chat message cannot be empty.", nameof(request));
        }

        var chatSession = await GetOrCreateChatSessionAsync(request.ConversationSessionId, cancellationToken);
        var conversationHistory = await _messageRepository.GetByChatSessionIdAsync(chatSession.Id, cancellationToken);

        await SaveMessageAsync(
            chatSession.Id,
            MessageRole.User,
            request.Message,
            cancellationToken);

        var prompt = await BuildPromptAsync(
            request.Message,
            conversationHistory,
            cancellationToken);

        var assistantReply = await _geminiService.GenerateResponseAsync(prompt, cancellationToken);

        await SaveMessageAsync(
            chatSession.Id,
            MessageRole.Assistant,
            assistantReply,
            cancellationToken);

        _logger.LogInformation(
            "Processed message for chat session {ChatSessionId}.",
            chatSession.Id);

        return new ChatResponseDto(
            chatSession.Id,
            assistantReply,
            DetectedIntent: null,
            ConfidenceScore: null,
            RequiresHumanEscalation: chatSession.Status == ChatSessionStatus.Escalated);
    }

    public async Task<IReadOnlyList<ChatSessionDto>> GetChatSessionsAsync(
        CancellationToken cancellationToken = default)
    {
        var sessions = await _chatSessionRepository.ListAsync(cancellationToken);

        return sessions
            .Select(session => new ChatSessionDto(
                session.Id,
                session.Status,
                session.Channel,
                session.CreatedAtUtc,
                session.EndedAtUtc))
            .ToList();
    }

    public async Task<IReadOnlyList<ChatMessageDto>> GetConversationMessagesAsync(
        Guid conversationSessionId,
        CancellationToken cancellationToken = default)
    {
        _ = await _chatSessionRepository.GetByIdAsync(conversationSessionId, cancellationToken)
            ?? throw new NotFoundException($"Chat session '{conversationSessionId}' was not found.");

        var messages = await _messageRepository.GetByChatSessionIdAsync(conversationSessionId, cancellationToken);

        return messages
            .Select(message => new ChatMessageDto(
                message.Id,
                message.ChatSessionId,
                message.Role,
                message.Content,
                message.IntentName,
                message.ConfidenceScore,
                message.CreatedAtUtc))
            .ToList();
    }

    public async Task EscalateToHumanAsync(
        Guid conversationSessionId,
        CancellationToken cancellationToken = default)
    {
        var chatSession = await _chatSessionRepository.GetByIdAsync(conversationSessionId, cancellationToken)
            ?? throw new NotFoundException($"Chat session '{conversationSessionId}' was not found.");

        chatSession.Status = ChatSessionStatus.Escalated;
        _chatSessionRepository.Update(chatSession);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Escalated chat session {ChatSessionId} to a human receptionist.",
            conversationSessionId);
    }

    private async Task<ChatSession> GetOrCreateChatSessionAsync(
        Guid? chatSessionId,
        CancellationToken cancellationToken)
    {
        if (chatSessionId is null)
        {
            var createdSessionId = await CreateChatSessionAsync(cancellationToken);
            return await _chatSessionRepository.GetByIdAsync(createdSessionId, cancellationToken)
                ?? throw new InvalidOperationException("Created chat session could not be loaded.");
        }

        return await _chatSessionRepository.GetByIdAsync(chatSessionId.Value, cancellationToken)
            ?? throw new NotFoundException($"Chat session '{chatSessionId}' was not found.");
    }

    private async Task<string> BuildPromptAsync(
        string currentUserMessage,
        IReadOnlyCollection<Message> conversationHistory,
        CancellationToken cancellationToken)
    {
        var companyInformation = await _companyInformationRepository.GetActiveAsync(cancellationToken);
        var faqs = await _faqRepository.SearchActiveAsync(currentUserMessage, cancellationToken);

        var promptRequest = new PromptBuilderRequest(
            MapCompanyInformation(companyInformation),
            faqs.Take(MaxPromptFaqs).Select(MapFaq).ToList(),
            conversationHistory.Select(MapConversationMessage).ToList(),
            currentUserMessage);

        return _promptBuilderService.BuildPrompt(promptRequest);
    }

    private async Task SaveMessageAsync(
        Guid chatSessionId,
        MessageRole role,
        string content,
        CancellationToken cancellationToken)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChatSessionId = chatSessionId,
            Role = role,
            Content = content.Trim()
        };

        await _messageRepository.AddAsync(message, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static PromptCompanyInformationDto? MapCompanyInformation(
        CompanyInformation? companyInformation)
    {
        return companyInformation is null
            ? null
            : new PromptCompanyInformationDto(
                companyInformation.CompanyName,
                companyInformation.Description,
                companyInformation.Address,
                companyInformation.PhoneNumber,
                companyInformation.Email,
                companyInformation.WebsiteUrl,
                companyInformation.BusinessHours);
    }

    private static PromptFaqDto MapFaq(Faq faq)
    {
        return new PromptFaqDto(
            faq.Question,
            faq.Answer,
            faq.Category);
    }

    private static PromptConversationMessageDto MapConversationMessage(Message message)
    {
        return new PromptConversationMessageDto(
            message.Role.ToString(),
            message.Content,
            message.CreatedAtUtc);
    }
}
