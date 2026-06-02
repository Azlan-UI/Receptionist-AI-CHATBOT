namespace ReceptionistChatBot.Application.DTOs.Prompts;

public sealed record PromptFaqDto(
    string Question,
    string Answer,
    string? Category);
