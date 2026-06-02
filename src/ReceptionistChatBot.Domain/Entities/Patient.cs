using ReceptionistChatBot.Domain.Common;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class Patient : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public string? Notes { get; set; }
}
