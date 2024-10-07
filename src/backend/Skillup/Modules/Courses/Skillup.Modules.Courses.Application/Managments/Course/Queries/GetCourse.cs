using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Application.Managments.Course.Queries
{
    public class GetCourse : IRequest<IEnumerable<CourseDto>>
    {
    }
}
