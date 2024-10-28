using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Users
{
    public record AddUserPurchasedCourseRequest(Guid CourseId, Guid UserId) : IRequest;
}
