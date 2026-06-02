using ReceptionistChatBot.Application.DTOs.Admin;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IFaqService
{
    Task<IReadOnlyList<FaqDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FaqDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<FaqDto> CreateAsync(CreateFaqRequest request, CancellationToken cancellationToken = default);
    Task<FaqDto?> UpdateAsync(Guid id, UpdateFaqRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
