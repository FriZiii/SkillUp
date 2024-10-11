using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdRequest, CourseDetailDto>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseByIdHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CourseDetailDto> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId);
            if (course == null) throw new Exception(); //TODO: Custom exception for nullable results from repo

            var courseMapper = new CourseMapper();

            var courseDto = courseMapper.CourseToCourseDetailDto(course);
            return courseDto;
        }
    }
}
