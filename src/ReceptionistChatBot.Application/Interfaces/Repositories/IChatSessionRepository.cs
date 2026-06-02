using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IChatSessionRepository : IRepository<ChatSession>
{
    Task<IReadOnlyList<ChatSession>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChatSession>> GetByStatusAsync(ChatSessionStatus status, CancellationToken cancellationToken = default);
}
