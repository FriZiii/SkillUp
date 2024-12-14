namespace Skillup.Modules.Courses.Core.DTO.Review
{
    public class CourseReviewCommentDto
    {
        public Guid Id { get; set; }
        public Guid CourseElementId { get; set; }
        public string Comment { get; set; }
        public bool IsResolved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
