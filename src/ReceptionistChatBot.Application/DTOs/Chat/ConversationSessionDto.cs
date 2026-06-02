using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.DTOs.Chat;

public sealed record ConversationSessionDto(
    Guid Id,
    Guid? PatientId,
    ConversationChannel Channel,
    string? VisitorName,
    string? VisitorContact,
    bool IsEscalatedToHuman,
    DateTime CreatedAtUtc,
    DateTime? ClosedAtUtc);
