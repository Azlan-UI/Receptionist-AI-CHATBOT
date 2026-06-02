namespace ReceptionistChatBot.Application.DTOs.Staff;

public sealed record StaffMemberDto(
    Guid Id,
    string FullName,
    string RoleTitle,
    string? Email,
    Guid DepartmentId,
    bool IsAvailableForAppointments);
