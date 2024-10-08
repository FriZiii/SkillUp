using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Course
    {
        public Guid Id { get; set; }
        //public Guid AuthorId { get; set; }      //authorsID
        //public Author Author { get; set; }
        public CourseInfo Info { get; set; }   //course information (title, subtitle, description)

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }    // category of course (ex. languages / programming)
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }  // subcategory of course (ex. english / databases)

        public CourseDetails? Details { get; set; } //lists of what student will learn, what they need to know before, and for whom is this course intended for

        //public string Language { get; set; } 
        public Uri ThumbnailUrl { get; set; } //minature photo of course

        public List<Section>? Sections { get; set; }
    }
}
