using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuizQuestionRequest(string Question, List<string> Answers, List<bool> Correct) : IRequest<QuizQuestionDto>
    {

        [JsonIgnore]
        public Guid AssignmentId;


    }
}
