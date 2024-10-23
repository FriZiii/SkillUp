using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record AddUserPurchasedCourseRequest(Guid CourseId, Guid UserId) : IRequest;
}
