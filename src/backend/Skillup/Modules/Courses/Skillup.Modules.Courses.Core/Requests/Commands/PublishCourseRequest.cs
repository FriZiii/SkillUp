using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record class PublishCourseRequest(Guid CourseId) : IRequest;
}
