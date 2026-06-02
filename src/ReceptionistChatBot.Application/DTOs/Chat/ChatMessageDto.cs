using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.DTOs.Chat;

public sealed record ChatMessageDto(
    Guid Id,
    Guid ConversationSessionId,
    MessageRole Role,
    string Content,
    string? IntentName,
    decimal? ConfidenceScore,
    DateTime CreatedAtUtc);
