using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetSectionsRequest(Guid CourseId) : IRequest<List<SectionDto>>;
}
