namespace Skillup.Modules.Chat.Core.Entities
{
    internal class Message
    {
        public Guid Id { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        public Guid SenderId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }
    }
}
