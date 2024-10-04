namespace Skillup.Modules.Courses.Core.Entities
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
        public CourseLevel Level { get; set; }     // level of difficulty (advanced, Beginner, Intermediate)

    }
}
