namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IGeminiService
{
    Task<string> GenerateResponseAsync(
        string prompt,
        CancellationToken cancellationToken);
}
