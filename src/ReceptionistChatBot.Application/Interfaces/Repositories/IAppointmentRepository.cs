using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IReadOnlyList<Appointment>> GetUpcomingByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByDateRangeAsync(DateTime startsAtUtc, DateTime endsAtUtc, CancellationToken cancellationToken = default);
}
