using ReceptionistChatBot.Domain.Common;
using ReceptionistChatBot.Domain.Enums;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public UserRole Role { get; set; } = UserRole.Visitor;
    public bool IsActive { get; set; } = true;

    public ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
}
