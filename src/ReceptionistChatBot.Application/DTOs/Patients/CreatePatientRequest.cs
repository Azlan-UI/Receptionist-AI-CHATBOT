namespace ReceptionistChatBot.Application.DTOs.Patients;

public sealed record CreatePatientRequest(
    string FullName,
    string? Email,
    string PhoneNumber,
    DateOnly? DateOfBirth,
    string? Notes);
