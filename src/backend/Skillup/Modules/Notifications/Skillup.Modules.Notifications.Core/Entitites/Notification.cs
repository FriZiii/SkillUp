using Skillup.Shared.Abstractions.Events.Notifications;

namespace Skillup.Modules.Notifications.Core.Entitites
{
    internal class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public NotifitationType Type { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
    }
}
