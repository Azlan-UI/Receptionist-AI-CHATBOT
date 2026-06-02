namespace ReceptionistChatBot.Application.DTOs.Chat;

public sealed record ChatRequestDto(
    Guid? ConversationSessionId,
    string Message,
    string? VisitorName,
    string? VisitorContact);
