using ReceptionistChatBot.Domain.Common;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
