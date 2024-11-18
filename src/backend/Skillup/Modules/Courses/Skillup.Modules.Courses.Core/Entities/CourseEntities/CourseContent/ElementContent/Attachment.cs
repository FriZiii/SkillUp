namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public Guid Key { get; set; }

        public Guid ElementId { get; set; }
        public Element Element { get; set; }
    }
}
