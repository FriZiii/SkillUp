using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record AddSectionRequest : IRequest
    {
        public string Title { get; set; }
        public Guid CourseId { get; set; }

        [JsonIgnore]
        public Guid SectionId { get; set; }
    }
}
