namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public bool IsPublished { get; set; } // only when published the section is visible for students
        //public bool IsCompleted { get; set; } //section is finished when all elemnets of the section are finished

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public List<Element> Elements { get; set; }  //element ex. one movie clip
    }
}
