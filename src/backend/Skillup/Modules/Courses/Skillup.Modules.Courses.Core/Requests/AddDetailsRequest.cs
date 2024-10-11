using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public record AddDetailsRequest(string Subtitle, string Description, CourseLevel Level,
        List<string> ObjectivesSummary, List<string> MustKnowBefore, List<string> IntendedFor, string? ThumbnailUrl) : IRequest
    {
        [JsonIgnore]
        public Guid CoruseId { get; set; }
    }
}
