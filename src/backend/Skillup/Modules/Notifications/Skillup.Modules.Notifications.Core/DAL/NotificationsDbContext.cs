using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Notifications.Core.Entitites;

namespace Skillup.Modules.Notifications.Core.DAL
{
    internal class NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("notifications");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
