using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class ChatSession : BaseEntity
{
    public Guid? UserId { get; set; }
    public ChatSessionStatus Status { get; set; } = ChatSessionStatus.Active;
    public string? Channel { get; set; }
    public string? VisitorIpAddress { get; set; }
    public string? Summary { get; set; }
    public DateTime? EndedAtUtc { get; set; }

    public User? User { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
