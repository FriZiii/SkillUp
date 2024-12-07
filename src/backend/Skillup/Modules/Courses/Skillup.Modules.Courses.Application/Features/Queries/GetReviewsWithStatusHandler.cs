using MassTransit.Initializers;
using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Review;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetReviewsWithStatusHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetReviewsWithStatusRequest, IEnumerable<CourseReviewDto>>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<IEnumerable<CourseReviewDto>> Handle(GetReviewsWithStatusRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseReviewMapper();
            var reviews = await _courseReviewRepository.GetWithStatus(request.Status);
            return reviews.Select(mapper.CourseReviewToDto);
        }
    }
}
