using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Sections
{
    public record AddSectionRequest(string Title, int index) : IRequest
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }

        [JsonIgnore]
        public Guid CourseId { get; set; }
    }
}
