using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Chat.Core.Entities;
using Skillup.Modules.Chat.Core.Repositories;

namespace Skillup.Modules.Chat.Core.DAL.Repositories
{
    internal class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;
        private readonly DbSet<Entities.Chat> _chats;
        private DbSet<Message> _messages;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
            _chats = context.Chats;
            _messages = context.Messages;
        }

        public async Task Add(Entities.Chat chat)
        {
            await _chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task AddMessage(Message message)
        {
            await _messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid chatId)
            => await _messages.Where(x => x.ChatId == chatId).ToListAsync();

        public async Task<IEnumerable<Entities.Chat>> GetChatsByCourseId(Guid courseId)
            => await _chats.Where(x => x.CourseId == courseId)
                .Include(x => x.Messages)
                .ToListAsync();

        public async Task<IEnumerable<Entities.Chat>> GetChatsByUserId(Guid userId)
            => await _chats.Where(x => x.UserId == userId || x.AuthorId == userId)
                .Include(x => x.Messages)
                .ToListAsync();
    }
}
