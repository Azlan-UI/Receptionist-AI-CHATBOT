using ReceptionistChatBot.Application.DTOs.Chat;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IChatService
{
    Task<Guid> CreateChatSessionAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChatSessionDto>> GetChatSessionsAsync(CancellationToken cancellationToken = default);
    Task<ChatResponseDto> SendMessageAsync(ChatRequestDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChatMessageDto>> GetConversationMessagesAsync(Guid conversationSessionId, CancellationToken cancellationToken = default);
    Task EscalateToHumanAsync(Guid conversationSessionId, CancellationToken cancellationToken = default);
}
