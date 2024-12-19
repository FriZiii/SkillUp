using Skillup.Modules.Courses.Core.DTO.User;

namespace Skillup.Modules.Courses.Core.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid ElementId { get; set; }
        public UserDto Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
        public ICollection<CommentDto> Replies { get; set; } = [];
    }
}
