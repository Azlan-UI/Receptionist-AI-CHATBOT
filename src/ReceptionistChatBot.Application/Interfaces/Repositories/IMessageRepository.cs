using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    Task<IReadOnlyList<Message>> GetByChatSessionIdAsync(Guid chatSessionId, CancellationToken cancellationToken = default);
    Task DeleteByChatSessionIdAsync(Guid chatSessionId, CancellationToken cancellationToken = default);
}
