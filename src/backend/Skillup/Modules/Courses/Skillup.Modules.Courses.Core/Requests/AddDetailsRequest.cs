using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public record AddDetailsRequest : IRequest
    {
        public Guid CoruseId { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public List<string> ObjectivesSummary { get; set; }
        public List<string> MustKnowBefore { get; set; }
        public List<string> IntendedFor { get; set; }
    }
}
