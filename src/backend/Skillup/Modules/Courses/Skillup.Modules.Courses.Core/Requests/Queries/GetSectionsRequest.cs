using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public class GetSectionsRequest : IRequest<List<SectionDto>>
    {
        public Guid CourseId { get; set; }
    }
}
