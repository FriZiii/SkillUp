using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Ratings
{
    public record class AddCourseRatingRequest : IRequest
    {
        [JsonIgnore]
        public Guid CourseId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public Guid RatingId { get; set; }

        public int Stars { get; set; }
        public string Feedback { get; set; }
    }
}
