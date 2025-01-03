using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Review;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetLatestReviewByCourseIdHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetLatestReviewByCourseIdRequest, CourseReviewDto>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<CourseReviewDto> Handle(GetLatestReviewByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseReviewMapper();
            var latestReview = await _courseReviewRepository.GetLatestByCourse(request.CourseId) ?? throw new NotFoundException($"Review for course with ID {request.CourseId} not found");
            return mapper.CourseReviewToDto(latestReview);
        }
    }
}
