using MediatR;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCoursesByAuthorIdRequest(Guid AuthorId, CourseStatus Status) : IRequest<List<CourseDto>>;
}
