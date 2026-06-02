using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<Patient?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(patient => patient.PhoneNumber == phoneNumber, cancellationToken);
    }

    public Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(patient => patient.Email == email, cancellationToken);
    }
}
