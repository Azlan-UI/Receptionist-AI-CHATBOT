using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public Guid? StaffMemberId { get; set; }
    public Guid? DepartmentId { get; set; }
    public DateTime StartsAtUtc { get; set; }
    public DateTime EndsAtUtc { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Reason { get; set; }
}
