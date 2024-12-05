using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class ReviewComment
    {
        public Guid Id { get; set; }

        public Guid CourseReviewId { get; set; }
        public CourseReview CourseReview { get; set; }

        public Guid CourseElementId { get; set; }
        public Element CourseElement { get; set; }

        public string CommentText { get; set; }
        public bool IsResolved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
