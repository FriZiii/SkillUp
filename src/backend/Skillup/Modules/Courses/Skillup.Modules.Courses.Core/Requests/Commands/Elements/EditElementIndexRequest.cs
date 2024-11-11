using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record EditElementIndexRequest(int index) : IRequest<SectionDto>
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }
    }
}
