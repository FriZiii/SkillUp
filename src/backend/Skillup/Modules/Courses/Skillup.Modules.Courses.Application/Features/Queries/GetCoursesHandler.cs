using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCoursesHandler : IRequestHandler<GetCoursesRequest, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseMapper();
            var courses = await _courseRepository.GetAll();
            var coursesDtos = courses.Select(mapper.CourseToCourseDto).ToList();
            return coursesDtos;
        }
    }
}
