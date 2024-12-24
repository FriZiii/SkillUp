using Skillup.Modules.Chat.Core.Entities;

namespace Skillup.Modules.Chat.Core.Repositories
{
    internal interface IChatRepository
    {
        Task Add(Entities.Chat chat);
        Task<IEnumerable<Entities.Chat>> GetChatsByUserId(Guid userId);
        Task<IEnumerable<Entities.Chat>> GetChatsByCourseId(Guid courseId);
        Task AddMessage(Message chatHistory);
        Task<IEnumerable<Message>> GetMessages(Guid chatId);
    }
}
