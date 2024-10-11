using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record EditDetailsRequest(string Subtitle, string Description, CourseLevel Level,
        List<string> ObjectivesSummary, List<string> MustKnowBefore, List<string> IntendedFor) : IRequest
    {
        [JsonIgnore]
        public Guid CourseId { get; set; }
    }
}
