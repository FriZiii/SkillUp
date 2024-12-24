namespace Skillup.Modules.Notifications.Core.Entitites
{
    internal class User
    {
        public Guid Id { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
