using ReceptionistChatBot.Application.DTOs.KnowledgeBase;

namespace ReceptionistChatBot.Application.Interfaces.Services;

public interface IKnowledgeBaseService
{
    Task<KnowledgeBaseArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<KnowledgeBaseArticleDto>> SearchPublishedAsync(string query, CancellationToken cancellationToken = default);
}
