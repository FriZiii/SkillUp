using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record class EditCourseStatusRequest(Guid CourseId, CourseStatus Status) : IRequest;
}
