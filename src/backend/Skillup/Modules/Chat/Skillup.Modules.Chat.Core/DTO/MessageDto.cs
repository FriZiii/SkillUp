namespace Skillup.Modules.Chat.Core.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public bool SendedByYou { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }
    }
}
