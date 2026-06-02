using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IConversationSessionRepository : IRepository<ConversationSession>
{
    Task<IReadOnlyList<ConversationSession>> GetOpenSessionsAsync(CancellationToken cancellationToken = default);
}
