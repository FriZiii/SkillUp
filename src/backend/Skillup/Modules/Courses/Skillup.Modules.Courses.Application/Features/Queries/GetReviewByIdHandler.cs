using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Review;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetReviewByIdHandler(ICourseReviewRepository courseReviewRepository) : IRequestHandler<GetReviewByIdRequest, CourseReviewDto>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;

        public async Task<CourseReviewDto> Handle(GetReviewByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseReviewMapper();
            var review = await _courseReviewRepository.Get(request.Id) ?? throw new NotFoundException($"Review with ID {request.Id} not found");
            return mapper.CourseReviewToDto(review);
        }
    }
}
