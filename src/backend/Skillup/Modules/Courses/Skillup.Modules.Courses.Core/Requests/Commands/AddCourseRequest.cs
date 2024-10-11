using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record AddCourseRequest(string Title, Guid CategoryId, Guid SubcategoryId) : IRequest
    {
        [JsonIgnore]
        public Guid AuthorId { get; set; }

        [JsonIgnore]
        public Guid CourseID { get; set; }
    }
}
