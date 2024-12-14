using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record AddAssignmentAssetRequest(string Instruction) : IRequest<AssignmentAssetDto>
    {
        [JsonIgnore]
        public Guid ElementId;
        [JsonIgnore]
        public ExerciseType ExerciseType;
    }
}
