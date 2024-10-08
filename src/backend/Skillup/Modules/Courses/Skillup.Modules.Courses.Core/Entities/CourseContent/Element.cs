using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Core.Entities.CourseContent
{
    public class Element
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public bool IsCompleted { get; set; } // movies/lectures finished when clicked

        public bool IsFree { get; set; }  // is this section visible before buying the course?
        public bool IsPublished { get; set; } // only when published the section is visible for students

        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }      //asset meaning movie, article or exercise
    }
}
