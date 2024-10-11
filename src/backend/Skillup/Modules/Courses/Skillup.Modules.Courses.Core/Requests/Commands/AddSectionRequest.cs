using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record AddSectionRequest(Guid CourseId, string Title) : IRequest
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
    }
}
