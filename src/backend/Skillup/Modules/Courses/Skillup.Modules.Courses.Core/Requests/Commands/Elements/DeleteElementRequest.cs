
using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record DeleteElementRequest : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }
    }
}
