using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class KnowledgeBaseRepository : Repository<KnowledgeBaseArticle>, IKnowledgeBaseRepository
{
    public KnowledgeBaseRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<KnowledgeBaseArticle>> SearchPublishedAsync(string query, CancellationToken cancellationToken = default)
    {
        var normalizedQuery = query.Trim();

        return await DbSet
            .AsNoTracking()
            .Where(article =>
                article.IsPublished &&
                (EF.Functions.ILike(article.Title, $"%{normalizedQuery}%") ||
                 EF.Functions.ILike(article.Content, $"%{normalizedQuery}%") ||
                 EF.Functions.ILike(article.Category, $"%{normalizedQuery}%")))
            .OrderBy(article => article.Category)
            .ThenBy(article => article.Title)
            .ToListAsync(cancellationToken);
    }
}
