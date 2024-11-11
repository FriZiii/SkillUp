using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record AddElementRequest(string Title, string Description, AssetType Type, int Index) : IRequest
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
    }
}
