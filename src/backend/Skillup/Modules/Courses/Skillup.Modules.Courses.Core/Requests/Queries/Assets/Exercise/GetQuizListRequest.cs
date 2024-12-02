using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise
{
    public record GetQuizListRequest(Guid AssignmentId) : IRequest<IEnumerable<QuizQuestionDto>>;
}
