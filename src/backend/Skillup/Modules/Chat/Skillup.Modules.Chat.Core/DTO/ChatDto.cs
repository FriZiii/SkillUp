namespace Skillup.Modules.Chat.Core.DTO
{
    internal class ChatDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public Guid AuthorId { get; set; }
        public MessageDto? LastMessage { get; set; }
    }
}
