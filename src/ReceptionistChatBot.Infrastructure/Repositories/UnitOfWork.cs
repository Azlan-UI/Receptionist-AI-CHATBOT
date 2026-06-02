using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ReceptionistChatBotDbContext _dbContext;

    public UnitOfWork(ReceptionistChatBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
