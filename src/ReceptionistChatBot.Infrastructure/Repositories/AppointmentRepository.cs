using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Appointment>> GetUpcomingByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        return await DbSet
            .AsNoTracking()
            .Where(appointment => appointment.PatientId == patientId && appointment.StartsAtUtc >= utcNow)
            .OrderBy(appointment => appointment.StartsAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetByDateRangeAsync(DateTime startsAtUtc, DateTime endsAtUtc, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(appointment => appointment.StartsAtUtc >= startsAtUtc && appointment.StartsAtUtc < endsAtUtc)
            .OrderBy(appointment => appointment.StartsAtUtc)
            .ToListAsync(cancellationToken);
    }
}
