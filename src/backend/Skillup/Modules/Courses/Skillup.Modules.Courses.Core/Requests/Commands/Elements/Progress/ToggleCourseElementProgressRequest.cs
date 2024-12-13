using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements.Progress
{
    public record ToggleCourseElementProgressRequest(Guid UserId, Guid CourseId, Guid ElementId) : IRequest;
}
