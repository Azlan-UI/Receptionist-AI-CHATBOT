namespace ReceptionistChatBot.Application.DTOs.Prompts;

public sealed record PromptBuilderRequest(
    PromptCompanyInformationDto? CompanyInformation,
    IReadOnlyCollection<PromptFaqDto> FAQs,
    IReadOnlyCollection<PromptConversationMessageDto> ConversationHistory,
    string CurrentUserMessage);
