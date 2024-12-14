using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public CourseStatus Status { get; set; } = CourseStatus.Draft;
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public CourseDetails Details { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User Author { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();

        public Course(Guid authorId, string title, Guid categoryId, Guid subcategoryId, DateTime now)
        {
            AuthorId = authorId;
            Title = title;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CreatedAt = now;
            Details = new();
        }

        public Course(Guid id, Guid authorId, string title, Guid categoryId, Guid subcategoryId, DateTime now, CourseDetails details, CourseStatus status)
        {
            Id = id;
            AuthorId = authorId;
            Title = title;
            Status = status;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CreatedAt = now;
            Details = details;
        }

        private Course() { } // For ef core
    }

    public enum CourseStatus
    {
        Draft,
        SubmitedForReview,
        PendingReview,
        ChangesRequired,
        Published
    }
}
