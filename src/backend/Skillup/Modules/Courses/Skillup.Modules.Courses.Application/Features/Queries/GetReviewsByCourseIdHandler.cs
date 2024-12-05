using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetReviewsByCourseIdHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetReviewsByCourseIdRequest, IEnumerable<CourseReview>>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<IEnumerable<CourseReview>> Handle(GetReviewsByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _courseReviewRepository.GetByCourse(request.CourseId);
            return reviews;
        }
    }
}
