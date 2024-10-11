using MediatR;
using Skillup.Modules.Courses.Application.Operations;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public class GetCoursesRequest : IRequest<IEnumerable<CourseDto>>
    {
    }
}
