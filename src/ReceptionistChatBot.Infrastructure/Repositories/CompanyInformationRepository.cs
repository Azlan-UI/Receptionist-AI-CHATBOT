using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class CompanyInformationRepository : Repository<CompanyInformation>, ICompanyInformationRepository
{
    public CompanyInformationRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<CompanyInformation?> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return DbSet
            .AsNoTracking()
            .OrderByDescending(company => company.UpdatedAtUtc ?? company.CreatedAtUtc)
            .FirstOrDefaultAsync(company => company.IsActive, cancellationToken);
    }
}
