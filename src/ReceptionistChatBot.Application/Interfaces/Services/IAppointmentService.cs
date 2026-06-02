using ReceptionistChatBot.Application.DTOs.Appointments;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IAppointmentService
{
    Task<AppointmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AppointmentDto>> GetUpcomingByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, CancellationToken cancellationToken = default);
    Task<AppointmentDto?> UpdateStatusAsync(Guid id, UpdateAppointmentStatusRequest request, CancellationToken cancellationToken = default);
}
