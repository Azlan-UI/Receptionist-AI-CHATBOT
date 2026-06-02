using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.DTOs.Appointments;

public sealed record AppointmentDto(
    Guid Id,
    Guid PatientId,
    Guid? StaffMemberId,
    Guid? DepartmentId,
    DateTime StartsAtUtc,
    DateTime EndsAtUtc,
    AppointmentStatus Status,
    string? Reason);
