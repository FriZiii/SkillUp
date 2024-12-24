using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Chat.Core.Entities;

namespace Skillup.Modules.Chat.Core.DAL
{
    internal class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
    {
        public DbSet<Entities.Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("chat");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
