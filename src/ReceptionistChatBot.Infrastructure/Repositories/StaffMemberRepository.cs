using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Repositories;

public sealed class StaffMemberRepository : Repository<StaffMember>, IStaffMemberRepository
{
    public StaffMemberRepository(ReceptionistChatBotDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<StaffMember>> GetAvailableByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(staffMember =>
                staffMember.DepartmentId == departmentId &&
                staffMember.IsAvailableForAppointments)
            .OrderBy(staffMember => staffMember.FullName)
            .ToListAsync(cancellationToken);
    }
}
