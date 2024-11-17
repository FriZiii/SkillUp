using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element
{
    public class Element
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public AssetType AssetType { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; }

        public Asset Asset { get; set; }
    }
}
