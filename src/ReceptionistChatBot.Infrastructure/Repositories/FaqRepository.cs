using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class FaqRepository : Repository<Faq>, IFaqRepository
{
    public FaqRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Faq>> GetActiveByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(faq => faq.IsActive && faq.Category == category)
            .OrderBy(faq => faq.DisplayOrder)
            .ThenBy(faq => faq.Question)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Faq>> SearchActiveAsync(string query, CancellationToken cancellationToken = default)
    {
        var normalizedQuery = query.Trim();

        return await DbSet
            .AsNoTracking()
            .Where(faq =>
                faq.IsActive &&
                (EF.Functions.ILike(faq.Question, $"%{normalizedQuery}%") ||
                 EF.Functions.ILike(faq.Answer, $"%{normalizedQuery}%") ||
                 EF.Functions.ILike(faq.Category, $"%{normalizedQuery}%") ||
                 (faq.Keywords != null && EF.Functions.ILike(faq.Keywords, $"%{normalizedQuery}%"))))
            .OrderBy(faq => faq.DisplayOrder)
            .ThenBy(faq => faq.Question)
            .ToListAsync(cancellationToken);
    }
}
