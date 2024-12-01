using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record AddAssignmentAssetRequest(Guid ElementId, ExerciseType ExerciseType, string Instruction) : IRequest<AssignmentAssetDto>
    {
    }
}
