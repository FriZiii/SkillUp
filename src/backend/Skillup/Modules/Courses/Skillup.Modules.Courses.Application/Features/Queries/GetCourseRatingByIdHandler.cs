using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCourseRatingByIdHandler(ICourseRatingRepository courseRatingRepository) : IRequestHandler<GetCourseRatingByIdRequest, CourseUserRatingDto>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;

        public async Task<CourseUserRatingDto> Handle(GetCourseRatingByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseRatingMapper();
            var rating = await _courseRatingRepository.GetById(request.Id) ?? throw new NotFoundException($"Rating with ID {request.Id} not found");
            return mapper.CourseRatingToCourseUserRatingDto(rating);
        }
    }
}
