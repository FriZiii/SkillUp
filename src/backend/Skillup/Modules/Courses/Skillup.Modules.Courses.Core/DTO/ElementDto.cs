using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.DTO
{
    public class ElementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AssetType Type { get; set; }
        public int Index { get; set; }
        public bool IsFree { get; set; }
    }
}
