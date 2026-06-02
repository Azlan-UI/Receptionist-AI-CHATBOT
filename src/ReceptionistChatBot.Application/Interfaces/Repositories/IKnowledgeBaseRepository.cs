using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IKnowledgeBaseRepository : IRepository<KnowledgeBaseArticle>
{
    Task<IReadOnlyList<KnowledgeBaseArticle>> SearchPublishedAsync(string query, CancellationToken cancellationToken = default);
}
