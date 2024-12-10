using MediatR;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCoursesRequest(CourseStatus? Status) : IRequest<IEnumerable<CourseDto>>;
}
