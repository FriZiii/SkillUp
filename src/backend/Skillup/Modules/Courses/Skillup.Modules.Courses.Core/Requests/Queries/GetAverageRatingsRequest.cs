using MediatR;
using Skillup.Modules.Courses.Core.DTO.Rating;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public class GetAverageRatingsRequest : IRequest<IEnumerable<CourseAverageRatingDto>>;
}
