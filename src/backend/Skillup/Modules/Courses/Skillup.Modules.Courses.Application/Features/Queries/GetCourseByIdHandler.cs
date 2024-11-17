using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdRequest, CourseDetailDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAmazonS3Service _amazonS3Service;

        public GetCourseByIdHandler(ICourseRepository courseRepository, IAmazonS3Service amazonS3Service)
        {
            _courseRepository = courseRepository;
            _amazonS3Service = amazonS3Service;
        }

        public async Task<CourseDetailDto> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId);
            if (course == null) throw new Exception(); //TODO: Custom exception for nullable results from repo

            var courseMapper = new CourseMapper(_amazonS3Service);

            var courseDto = courseMapper.CourseToCourseDetailDto(course);
            return courseDto;
        }
    }
}
