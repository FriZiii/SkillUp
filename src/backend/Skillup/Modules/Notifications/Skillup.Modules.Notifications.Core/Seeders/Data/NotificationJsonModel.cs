namespace Skillup.Modules.Notifications.Core.Seeders.Data
{
    internal class NotificationJsonModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
    }
}
