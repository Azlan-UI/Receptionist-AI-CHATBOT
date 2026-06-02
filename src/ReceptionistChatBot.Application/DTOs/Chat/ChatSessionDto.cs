using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Application.DTOs.Chat;

public sealed record ChatSessionDto(
    Guid Id,
    ChatSessionStatus Status,
    string? Channel,
    DateTime CreatedAtUtc,
    DateTime? EndedAtUtc);
