using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuestionAnswerRequest(string Question, string Answer) : IRequest<QuestionAnswerDto>
    {
        [JsonIgnore]
        public Guid AssignmentId;
    }
}
