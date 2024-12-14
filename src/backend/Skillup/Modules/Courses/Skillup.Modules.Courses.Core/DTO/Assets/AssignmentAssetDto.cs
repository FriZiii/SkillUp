using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Core.DTO.Assets
{
    public class AssignmentAssetDto : AssetDto
    {
        public string Instruction { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
