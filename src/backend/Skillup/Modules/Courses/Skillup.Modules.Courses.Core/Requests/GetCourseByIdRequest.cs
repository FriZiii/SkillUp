using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests
{
    public record GetCourseByIdRequest(Guid CourseId) : IRequest<CourseDetailDto>;
}
