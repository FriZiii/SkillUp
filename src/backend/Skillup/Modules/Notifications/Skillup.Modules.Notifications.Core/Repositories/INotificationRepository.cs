using Skillup.Modules.Notifications.Core.Entitites;

namespace Skillup.Modules.Notifications.Core.Repositories
{
    internal interface INotificationRepository
    {
        Task Add(Notification notification);
        Task<IEnumerable<Notification>> GetByUserId(Guid userId);
        Task MarkAsSeen(Guid notificationId);
    }
}
