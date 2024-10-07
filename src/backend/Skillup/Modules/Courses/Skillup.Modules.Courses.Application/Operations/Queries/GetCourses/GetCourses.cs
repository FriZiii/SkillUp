using MediatR;

namespace Skillup.Modules.Courses.Application.Operations.Queries.GetCourses
{
    public class GetCourses : IRequest<IEnumerable<CourseDto>>
    {
    }
}
