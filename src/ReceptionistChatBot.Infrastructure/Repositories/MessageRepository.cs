using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Message>> GetByChatSessionIdAsync(Guid chatSessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(message => message.ChatSessionId == chatSessionId)
            .OrderBy(message => message.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
