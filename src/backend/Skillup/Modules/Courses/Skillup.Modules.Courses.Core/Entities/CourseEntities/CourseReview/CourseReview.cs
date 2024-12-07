namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class CourseReview
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? FinalizedAt { get; set; }
        public ReviewStatus Status { get; set; }

        public IEnumerable<CourseReviewComment> Comments { get; set; }
    }

    public enum ReviewStatus
    {
        Finalized,
        FinalizedWithRequiredChanges,
        InProgress
    }
}
