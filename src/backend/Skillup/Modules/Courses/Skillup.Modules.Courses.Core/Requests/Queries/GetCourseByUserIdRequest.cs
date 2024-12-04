using MediatR;
using Skillup.Modules.Courses.Application.Operations;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public record GetCourseByUserIdRequest(Guid UserId) : IRequest<IEnumerable<CourseDto>>;
}
