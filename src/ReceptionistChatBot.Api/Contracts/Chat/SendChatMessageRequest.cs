using System.ComponentModel.DataAnnotations;

namespace ReceptionistChatBot.Api.Contracts.Chat;

public sealed record SendChatMessageRequest
{
    public Guid? SessionId { get; init; }

    [StringLength(150, MinimumLength = 2)]
    public string? VisitorName { get; init; }

    [StringLength(100)]
    public string? VisitorContact { get; init; }

    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Message { get; init; } = string.Empty;
}
