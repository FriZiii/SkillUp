﻿using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Operations.Queries.GetCourses
{
    public class GetCoursesHandler : IRequestHandler<GetCourses, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<CourseDto>> Handle(GetCourses request, CancellationToken cancellationToken)
        {
            var mapper = new CourseMapper();
            var courses = await _courseRepository.GetAll();
            var coursesDtos = courses.Select(c => mapper.CourseToCourseDto(c)).ToList();
            return coursesDtos;
        }
    }
}
