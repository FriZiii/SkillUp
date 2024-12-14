using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCourseRatingByIdHandler(ICourseRatingRepository courseRatingRepository) : IRequestHandler<GetCourseRatingByIdRequest, CourseUserRatingDto>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;

        public async Task<CourseUserRatingDto> Handle(GetCourseRatingByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseRatingMapper();
            var rating = await _courseRatingRepository.GetById(request.Id) ?? throw new Exception(); // TODO: Custom ex: rating with id doesnt exist
            return mapper.CourseRatingToCourseUserRatingDto(rating);
        }
    }
}
