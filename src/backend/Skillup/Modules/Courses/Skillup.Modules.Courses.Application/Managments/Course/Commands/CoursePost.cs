using Skillup.Modules.Courses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Application.Managments.Course.Commands
{
    public class CoursePost
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public CourseLevel Level { get; set; }
        public List<string> ObjectivesSummary { get; set; }
        public List<string> MustKnowBefore { get; set; }
        public List<string> IntendedFor { get; set; }
        public Uri ThumbnailUrl { get; set; }
        public decimal Price { get; set; }
    }
}
