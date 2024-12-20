using MediatR;
using Skillup.Modules.Courses.Core.DTO.Rating;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetRatingsByCourseIdRequest(Guid CourseId, int? LatestUserRatingsCount) : IRequest<CourseDetailedRatingDto?>;
}
