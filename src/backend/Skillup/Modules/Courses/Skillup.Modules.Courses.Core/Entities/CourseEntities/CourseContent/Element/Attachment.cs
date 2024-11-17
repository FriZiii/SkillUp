namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public Guid Key { get; set; }

        public Guid ElementId { get; set; }
        public Element Element { get; set; }
    }
}
