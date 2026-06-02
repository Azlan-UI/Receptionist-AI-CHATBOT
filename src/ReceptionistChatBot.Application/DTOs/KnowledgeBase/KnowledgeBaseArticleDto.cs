namespace ReceptionistChatBot.Application.DTOs.KnowledgeBase;

public sealed record KnowledgeBaseArticleDto(
    Guid Id,
    string Title,
    string Content,
    string Category,
    bool IsPublished);
