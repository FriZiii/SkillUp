using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuizAnswerRequest(Guid QuizId, string Answer, bool IsCorrect) : IRequest<QuizAnswerDto>;
}
