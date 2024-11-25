using MediatR;
using Skillup.Modules.Courses.Application.Operations;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record EditCourseRequest(string Title, Guid CategoryId, Guid SubcategoryId) : IRequest<CourseDto>
    {
        [JsonIgnore]
        public Guid CourseID { get; set; }
    }
}
