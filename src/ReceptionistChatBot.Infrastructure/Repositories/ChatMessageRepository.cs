using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ChatMessage>> GetByConversationSessionIdAsync(Guid conversationSessionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(message => message.ConversationSessionId == conversationSessionId)
            .OrderBy(message => message.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
