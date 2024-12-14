using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.DTO.Review
{
    public class CourseReviewDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinalizedAt { get; set; }
        public ReviewStatus Status { get; set; }

        public IEnumerable<CourseReviewCommentDto> Comments { get; set; }
    }
}
