using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuestionAnswerRequest(Guid AssignmentId, string Question, string Answer) : IRequest<QuestionAnswerDto>;
}
