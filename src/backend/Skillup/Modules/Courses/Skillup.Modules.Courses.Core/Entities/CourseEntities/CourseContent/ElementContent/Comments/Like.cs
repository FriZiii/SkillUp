using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid LikerId { get; set; }
        public User Liker { get; set; }

        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
