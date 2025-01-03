using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Notifications.Core.DAL.Repositories
{
    internal class NotificationRepository : INotificationRepository
    {
        private readonly NotificationsDbContext _context;
        private readonly DbSet<Notification> _notifications;

        public NotificationRepository(NotificationsDbContext context)
        {
            _context = context;
            _notifications = context.Notifications;
        }

        public async Task Add(Notification notification)
        {
            await _notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetByUserId(Guid userId)
            => await _notifications.Where(x => x.UserId == userId).ToListAsync();

        public async Task MarkAsSeen(Guid notificationId)
        {
            var notificationToEdit = await _notifications.FirstOrDefaultAsync(x => x.Id == notificationId) ?? throw new NotFoundException($"Notification with ID {notificationId} not found");
            notificationToEdit.Seen = true;
            await _context.SaveChangesAsync();
        }
    }
}
