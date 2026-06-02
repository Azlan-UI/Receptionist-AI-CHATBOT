namespace ReceptionistChatBot.Application.DTOs.Prompts;

public sealed record PromptConversationMessageDto(
    string Role,
    string Content,
    DateTime CreatedAtUtc);
