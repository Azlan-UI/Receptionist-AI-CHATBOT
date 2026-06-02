namespace ReceptionistChatBot.Application.DTOs.Staff;

public sealed record DepartmentDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive);
