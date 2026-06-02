using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
