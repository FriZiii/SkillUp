using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class AssignmentMapper
    {
        ExerciseMapper exerciseMapper = new ExerciseMapper();
        public AssignmentAssetDto AssignmentToAssignmentDto(Assignment assignment)
        {
            var assignmentDto = new AssignmentAssetDto()
            {
                AssetId = assignment.Id,
                ElementId = assignment.ElementId,
                ExerciseType = assignment.ExerciseType,
                Instruction = assignment.Instruction,
            };
            return assignmentDto;
        }


    }
}
