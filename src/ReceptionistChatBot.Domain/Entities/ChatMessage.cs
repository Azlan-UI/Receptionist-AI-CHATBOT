using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class ChatMessage : BaseEntity
{
    public Guid ConversationSessionId { get; set; }
    public MessageRole Role { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? IntentName { get; set; }
    public decimal? ConfidenceScore { get; set; }
}
