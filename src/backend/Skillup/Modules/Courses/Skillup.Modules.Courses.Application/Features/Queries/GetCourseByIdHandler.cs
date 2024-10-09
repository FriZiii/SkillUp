using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var courseMapper = new CourseMapper();

            var courseDto = courseMapper.CourseToCourseDetailDto(course);
            return courseDto;
        }
    }
}
