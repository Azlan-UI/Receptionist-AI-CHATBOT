namespace ReceptionistChatBot.Application.DTOs.Admin;

public sealed record FaqDto(
    Guid Id,
    string Question,
    string Answer,
    string Category,
    string? Keywords,
    int DisplayOrder,
    bool IsActive);
