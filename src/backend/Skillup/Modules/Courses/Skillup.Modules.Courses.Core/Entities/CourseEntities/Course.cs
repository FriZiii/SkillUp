using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Course
    {
        public Guid Id { get; set; }
        //TODO : public Guid AuthorId { get; set; } 
        public string Title { get; set; }   //course name (ex. full c# course from basics)
        public bool IsPublished { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public CourseDetails Details { get; set; } //lists of what student will learn, what they need to know before, and for whom is this course intended for
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public List<Section>? Sections { get; set; }

        public Course(string title, Guid categoryId, Guid subcategoryId, DateTime now)
        {
            Title = title;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CreatedAt = now;
            Details = new();
        }

        public Course(string title, Guid categoryId, Guid subcategoryId, DateTime now, CourseDetails details)
        {
            Title = title;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CreatedAt = now;
            Details = details;
        }

        private Course() { } // For ef core
    }
}
