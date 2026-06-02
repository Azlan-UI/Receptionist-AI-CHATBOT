using ReceptionistChatBot.Application.DTOs.Staff;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IStaffDirectoryService
{
    Task<IReadOnlyList<DepartmentDto>> GetActiveDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StaffMemberDto>> GetAvailableStaffByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);
}
