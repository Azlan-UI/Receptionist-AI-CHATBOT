using ReceptionistChatBot.Application.DTOs.Prompts;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IPromptBuilderService
{
    string BuildPrompt(PromptBuilderRequest request);
}
