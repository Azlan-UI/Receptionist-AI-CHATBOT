namespace ReceptionistChatBot.Application.DTOs.Admin;

public sealed record CompanyInformationDto(
    Guid Id,
    string CompanyName,
    string? Description,
    string? Address,
    string? PhoneNumber,
    string? Email,
    string? WebsiteUrl,
    string? BusinessHours,
    bool IsActive);
