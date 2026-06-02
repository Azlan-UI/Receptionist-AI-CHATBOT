using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IFaqRepository : IRepository<Faq>
{
    Task<IReadOnlyList<Faq>> GetActiveByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Faq>> SearchActiveAsync(string query, CancellationToken cancellationToken = default);
}
