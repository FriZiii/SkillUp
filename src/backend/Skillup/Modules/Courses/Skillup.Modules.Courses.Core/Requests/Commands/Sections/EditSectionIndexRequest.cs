
using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Sections
{
    public record EditSectionIndexRequest(int index) : IRequest<List<SectionDto>>
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
    }
}
