namespace ReceptionistChatBot.Application.DTOs.Appointments;

public sealed record CreateAppointmentRequest(
    Guid PatientId,
    Guid? StaffMemberId,
    Guid? DepartmentId,
    DateTime StartsAtUtc,
    DateTime EndsAtUtc,
    string? Reason);
