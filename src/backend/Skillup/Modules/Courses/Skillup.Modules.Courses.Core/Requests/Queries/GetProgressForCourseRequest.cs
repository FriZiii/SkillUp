using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetProgressForCourseRequest(Guid UserId, Guid CourseId) : IRequest<IEnumerable<Guid>>;
}
