namespace Skillup.Modules.Chat.Core.Entities
{
    internal class Chat
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public Guid AuthorId { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
