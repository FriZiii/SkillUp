using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCoursesHandler : IRequestHandler<GetCoursesRequest, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAmazonS3Service _amazonS3Service;

        public GetCoursesHandler(ICourseRepository courseRepository, IAmazonS3Service amazonS3Service)
        {
            _courseRepository = courseRepository;
            _amazonS3Service = amazonS3Service;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseMapper(_amazonS3Service);
            var courses = await _courseRepository.GetAll();
            var coursesDtos = courses.Select(mapper.CourseToCourseDto).ToList();
            return coursesDtos;
        }
    }
}
