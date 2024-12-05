using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetReviewByIdHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetReviewByIdRequest, CourseReview>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<CourseReview> Handle(GetReviewByIdRequest request, CancellationToken cancellationToken)
        {
            var review = await _courseReviewRepository.Get(request.Id) ?? throw new Exception(); // TODO: Custom ex
            return review;
        }
    }
}
