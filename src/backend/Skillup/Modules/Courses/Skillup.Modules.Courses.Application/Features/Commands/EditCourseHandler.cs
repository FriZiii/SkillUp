using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class EditCourseHandler : IRequestHandler<EditCourseRequest, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<EditCourseHandler> _logger;

        public EditCourseHandler(ICourseRepository courseRepository, ILogger<EditCourseHandler> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }
        public async Task<CourseDto> Handle(EditCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseID);
            course.Title = request.Title;
            course.CategoryId = request.CategoryId;
            course.SubcategoryId = request.SubcategoryId;

            await _courseRepository.Edit(course);
            _logger.LogInformation("Element edited");

            var newCourse = await _courseRepository.GetById(request.CourseID);
            var courseMapper = new CourseMapper();
            return courseMapper.CourseToCourseDto(newCourse);
        }
    }
}
