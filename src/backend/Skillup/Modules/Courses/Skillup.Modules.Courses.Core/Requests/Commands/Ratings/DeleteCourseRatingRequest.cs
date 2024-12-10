using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Ratings
{
    public record DeleteCourseRatingRequest(Guid Id) : IRequest;
}
