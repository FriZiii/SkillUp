using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements
{
    public record AddElementRequest(string Title, string Description, int Index) : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }

        [JsonIgnore]
        public Guid SectionId { get; set; }

        [JsonIgnore]
        public AssetType AssetType { get; set; }
    }
}
