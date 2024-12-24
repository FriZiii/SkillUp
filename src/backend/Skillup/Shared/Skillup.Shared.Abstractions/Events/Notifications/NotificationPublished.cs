namespace Skillup.Shared.Abstractions.Events.Notifications
{
    public record NotificationPublished(NotifitationType Type, Guid UserId, string Message);

    public enum NotifitationType
    {
        User, Instructor
    }
}
