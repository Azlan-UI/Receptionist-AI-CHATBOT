using ReceptionistChatBot.Domain.Common;

namespace ReceptionistChatBot.Domain.Entities;

public sealed class KnowledgeBaseArticle : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
