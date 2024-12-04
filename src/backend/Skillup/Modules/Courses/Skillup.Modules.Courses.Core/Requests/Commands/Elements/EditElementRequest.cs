using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record EditElementRequest(string Title, string Description, bool IsFree) : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }
    }
}
