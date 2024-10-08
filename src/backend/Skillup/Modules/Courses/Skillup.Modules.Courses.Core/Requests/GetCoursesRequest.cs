using MediatR;
using Skillup.Modules.Courses.Application.Operations;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class GetCoursesRequest : IRequest<IEnumerable<CourseDto>>
    {
    }
}
