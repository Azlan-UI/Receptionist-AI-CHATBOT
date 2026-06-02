using ReceptionistChatBot.Application.DTOs.Patients;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IPatientService
{
    Task<PatientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PatientDto?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<PatientDto> CreateAsync(CreatePatientRequest request, CancellationToken cancellationToken = default);
}
