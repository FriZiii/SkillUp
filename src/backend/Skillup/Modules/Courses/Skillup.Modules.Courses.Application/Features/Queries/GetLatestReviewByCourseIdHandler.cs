using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetLatestReviewByCourseIdHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetLatestReviewByCourseIdRequest, CourseReview>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<CourseReview> Handle(GetLatestReviewByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var latestReview = await _courseReviewRepository.GetLatestByCourse(request.CourseId) ?? throw new Exception(); // TODO: Custom ex
            return latestReview;
        }
    }
}
