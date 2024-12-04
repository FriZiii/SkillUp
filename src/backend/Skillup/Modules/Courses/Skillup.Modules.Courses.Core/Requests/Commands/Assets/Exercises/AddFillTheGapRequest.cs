using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddFillTheGapRequest(string Sentence, List<AddWord> Words) : IRequest<FillTheGapSentenceDto>
    {
        [JsonIgnore]
        public Guid AssignmentId;
    }

    public class AddWord
    {
        public string Value { get; set; }
        public int Index { get; set; }
    }
}
