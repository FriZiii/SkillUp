using MediatR;
using Skillup.Modules.Courses.Application.Operations;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCoursesRequest : IRequest<IEnumerable<CourseDto>>;
}
