using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record EditAssignmentAssetRequest(string Instruction) : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }
    }
}
