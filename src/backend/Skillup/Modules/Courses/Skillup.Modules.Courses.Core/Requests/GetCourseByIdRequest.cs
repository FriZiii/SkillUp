using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.Requests
{
    public class GetCourseByIdRequest : IRequest<CourseDetailDto>
    {
        public Guid CourseId { get; set; }
    }
}
