using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class Message : BaseEntity
{
    public Guid ChatSessionId { get; set; }
    public MessageRole Role { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? IntentName { get; set; }
    public decimal? ConfidenceScore { get; set; }
    public TimeSpan? ResponseTime { get; set; }
    public bool IsHelpful { get; set; }

    public ChatSession ChatSession { get; set; } = null!;
}
