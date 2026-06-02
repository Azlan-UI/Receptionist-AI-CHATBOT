namespace ReceptionistChatBot.Application.DTOs.Patients;

public sealed record PatientDto(
    Guid Id,
    string FullName,
    string? Email,
    string PhoneNumber,
    DateOnly? DateOfBirth,
    string? Notes);
