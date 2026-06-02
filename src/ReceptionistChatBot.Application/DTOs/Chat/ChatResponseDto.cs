namespace ReceptionistChatBot.Application.DTOs.Chat;

public sealed record ChatResponseDto(
    Guid ConversationSessionId,
    string Reply,
    string? DetectedIntent,
    decimal? ConfidenceScore,
    bool RequiresHumanEscalation);
