namespace Skillup.Modules.Courses.Core.Entities.CourseContent.Assets
{
    public class Asset
    {
        public Guid Id { get; set; }

        public Guid ElementId { get; set; }
        public Element Element { get; set; }

        //assettype
    }
}
