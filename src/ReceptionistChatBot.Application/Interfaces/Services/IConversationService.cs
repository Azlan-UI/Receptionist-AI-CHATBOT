using ReceptionistChatBot.Application.DTOs.Chat;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IConversationService
{
    Task<ConversationSessionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConversationSessionDto>> GetOpenSessionsAsync(CancellationToken cancellationToken = default);
    Task CloseAsync(Guid id, CancellationToken cancellationToken = default);
}
