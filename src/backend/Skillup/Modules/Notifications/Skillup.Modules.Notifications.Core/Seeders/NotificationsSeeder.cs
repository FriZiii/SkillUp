using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Notifications.Core.DAL;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Seeders.Data;
using Skillup.Shared.Abstractions.Events.Notifications;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;
using System.Text.Json;

namespace Skillup.Modules.Notifications.Core.Seeders
{
    internal class NotificationsSeeder : ISeeder
    {
        private readonly NotificationsDbContext _context;
        private readonly IClock _clock;
        private readonly DbSet<User> _users;
        private DbSet<Notification> _notifications;

        public NotificationsSeeder(NotificationsDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
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
                await _notifications.AddRangeAsync(CreateNotifications());
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> CreateUsers()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "notification-users-seeder-data.json"));
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

        private IEnumerable<Notification> CreateNotifications()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "notification-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<NotificationJsonModel>>(jsonString, options);

            return data!.Select(CreateNotificationFromJson);
        }

        private Notification CreateNotificationFromJson(NotificationJsonModel jsonModel)
        {
            var Notification = new Notification()
            {
                UserId = jsonModel.UserId,
                CreatedAt = _clock.CurrentDate(),
                Type = Enum.Parse<NotifitationType>(jsonModel.Type),
                Message = jsonModel.Message,
                Seen = jsonModel.Seen,
            };

            return Notification;
        }
    }
}
