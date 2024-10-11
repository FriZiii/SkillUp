using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Course
    {
        public Guid Id { get; set; }
        //TODO : public Guid AuthorId { get; set; } 
        public string Title { get; set; } = string.Empty;    //course name (ex. full c# course from basics)
        public bool IsPublished { get; set; } = false;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }    // category of course (ex. languages / programming)}  // subcategory of course (ex. english / databases)
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public CourseDetails Details { get; set; } = new(); //lists of what student will learn, what they need to know before, and for whom is this course intended for
        public DateTime CreatedAt { get; set; }

        public List<Section>? Sections { get; set; }
    }
}
