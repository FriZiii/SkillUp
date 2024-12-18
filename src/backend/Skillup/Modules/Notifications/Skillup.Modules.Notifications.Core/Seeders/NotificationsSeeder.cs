using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Notifications.Core.DAL;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Seeders.Data;
using Skillup.Shared.Abstractions.Seeder;
using System.Text.Json;

namespace Skillup.Modules.Notifications.Core.Seeders
{
    internal class NotificationsSeeder : ISeeder
    {
        private readonly NotificationsDbContext _context;
        private readonly DbSet<User> _users;
        private DbSet<Notification> _notifications;

        public NotificationsSeeder(NotificationsDbContext context)
        {
            _context = context;
            _users = _context.Users;
            _notifications = _context.Notifications;
        }
        public async Task Seed()
        {
            if (!await _users.AnyAsync())
            {
                await _users.AddRangeAsync(CreateUsers());
                await _context.SaveChangesAsync();
            }

            if (!await _notifications.AnyAsync())
            {
                // TODO: Seed notifications
            }
        }

        private IEnumerable<User> CreateUsers()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "notification-users-seeder-data"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var usersData = JsonSerializer.Deserialize<List<NotificationUserJsonModel>>(jsonString, options);

            return usersData!.Select(CreateUserFromJson);
        }

        private User CreateUserFromJson(NotificationUserJsonModel jsonModel)
        {
            var user = new User() { Id = jsonModel.Id };

            return user;
        }
    }
}
