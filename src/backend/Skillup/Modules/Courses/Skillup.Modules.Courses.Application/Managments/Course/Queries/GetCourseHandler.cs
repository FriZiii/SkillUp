using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Application.Managments.Course.Queries
{
    public class GetCourseHandler : IRequestHandler<GetCourse, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public Task<IEnumerable<CourseDto>> Handle(GetCourse request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
