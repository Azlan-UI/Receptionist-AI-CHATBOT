using ReceptionistChatBot.Domain.Common;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class CompanyInformation : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? BusinessHours { get; set; }
    public bool IsActive { get; set; } = true;
}
