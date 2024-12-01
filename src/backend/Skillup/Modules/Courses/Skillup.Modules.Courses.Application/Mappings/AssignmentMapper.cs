using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class AssignmentMapper
    {
        public AssignmentDto AssignmentToAssignmentDto(Assignment assignment)
        {
            var assignmentDto = new AssignmentDto()
            {
                AssetId = assignment.Id,
                ElementId = assignment.ElementId,
                Instruction = assignment.Instruction,
            };
            return assignmentDto;
        }
    }
}
