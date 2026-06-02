namespace ReceptionistChatBot.Application.DTOs.Prompts;

public sealed record PromptCompanyInformationDto(
    string CompanyName,
    string? Description,
    string? Address,
    string? PhoneNumber,
    string? Email,
    string? WebsiteUrl,
    string? BusinessHours);
