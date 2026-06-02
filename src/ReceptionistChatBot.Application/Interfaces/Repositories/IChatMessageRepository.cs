using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Application.Interfaces.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IReadOnlyList<ChatMessage>> GetByConversationSessionIdAsync(Guid conversationSessionId, CancellationToken cancellationToken = default);
}
