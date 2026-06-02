using ReceptionistChatBot.Application.DTOs.Admin;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface ICompanyInformationService
{
    Task<CompanyInformationDto?> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<CompanyInformationDto> UpsertAsync(UpdateCompanyInformationRequest request, CancellationToken cancellationToken = default);
}
