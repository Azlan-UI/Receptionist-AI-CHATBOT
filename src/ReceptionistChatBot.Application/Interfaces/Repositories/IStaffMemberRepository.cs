using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IStaffMemberRepository : IRepository<StaffMember>
{
    Task<IReadOnlyList<StaffMember>> GetAvailableByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);
}
