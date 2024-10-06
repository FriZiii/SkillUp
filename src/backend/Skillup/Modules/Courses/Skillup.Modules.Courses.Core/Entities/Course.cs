using Skillup.Modules.Courses.Core.Entities.CourseContent;

namespace Skillup.Modules.Courses.Core.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        //public Guid AuthorId { get; set; }      //authorsID
        //public Author Author { get; set; }
        public CourseInfo Info { get; set; }   //course information (title, subtitle, description)

        //
        // Summary:
        //     A read-only instance of the System.Guid structure whose value is all zeros.
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }    // category of course (ex. languages / programming)
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }  // subcategory of course (ex. english / databases)
        public CourseLevel Level { get; set; }     // level of difficulty (advanced, Beginner, Intermediate)


        public StringList ObjectivesSummary { get; set; }   //list of things that you will learn throughout the course (ex. you will learn how to program moblie apps)
        public StringList MustKnowBefore { get; set; }      //list of things you should already know before starting the course (ex. you should know basics of object-oriented progemming)
        public StringList IntendedFor { get; set; }         // for whom is this course intended for (ex. for everyone / people the know the basics of programming)

        //public string Language { get; set; } 
        public Uri ThumbnailUrl { get; set; } //minature photo of course
        public Price Price { get; set; }   //price of course

        public List<Section>? Sections { get; set; }
    }
}
