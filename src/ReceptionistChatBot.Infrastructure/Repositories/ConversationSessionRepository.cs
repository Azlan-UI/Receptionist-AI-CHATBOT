using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class ConversationSessionRepository : Repository<ConversationSession>, IConversationSessionRepository
{
    public ConversationSessionRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ConversationSession>> GetOpenSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(session => session.ClosedAtUtc == null)
            .OrderByDescending(session => session.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
