namespace ReceptionistChatBot.Application.DTOs.Admin;

public sealed record CreateFaqRequest(
    string Question,
    string Answer,
    string Category,
    string? Keywords,
    int DisplayOrder,
    bool IsActive);
