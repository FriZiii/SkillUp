namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets
{
    public abstract class Asset
    {
        public Guid Id { get; set; }

        public Guid ElementId { get; set; }
        public Element Element { get; set; }
    }
}
