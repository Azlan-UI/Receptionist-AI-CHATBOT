using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class ConversationSession : BaseEntity
{
    public Guid? PatientId { get; set; }
    public ConversationChannel Channel { get; set; } = ConversationChannel.Web;
    public string? VisitorName { get; set; }
    public string? VisitorContact { get; set; }
    public bool IsEscalatedToHuman { get; set; }
    public DateTime? ClosedAtUtc { get; set; }
}
