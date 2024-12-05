using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetReviewsWithStatusHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetReviewsWithStatusRequest, IEnumerable<CourseReview>>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public Task<IEnumerable<CourseReview>> Handle(GetReviewsWithStatusRequest request, CancellationToken cancellationToken)
        {
            var reviews = _courseReviewRepository.GetWithStatus(request.Status);
            return reviews;
        }
    }
}
