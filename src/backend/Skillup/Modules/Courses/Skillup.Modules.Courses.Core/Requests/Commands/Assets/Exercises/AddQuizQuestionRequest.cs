using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuizQuestionRequest(Guid AssignmentId, string Question) : IRequest<QuizQuestionDto>;
}
