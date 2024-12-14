
using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record DeleteExerciseRequest(Guid ExerciseId, ExerciseType ExerciseType) : IRequest;
}
