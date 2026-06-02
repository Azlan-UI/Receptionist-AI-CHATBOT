using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface ICompanyInformationRepository : IRepository<CompanyInformation>
{
    Task<CompanyInformation?> GetActiveAsync(CancellationToken cancellationToken = default);
}
