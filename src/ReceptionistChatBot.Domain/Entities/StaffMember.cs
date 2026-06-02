using ReceptionistChatBot.Domain.Common;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class StaffMember : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string RoleTitle { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Guid DepartmentId { get; set; }
    public bool IsAvailableForAppointments { get; set; }
}
