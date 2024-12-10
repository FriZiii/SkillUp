using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Ratings
{
    public record EditCourseRatingRequest(int Stars, string Feedback) : IRequest
    {
        [JsonIgnore]
        public Guid RatingId { get; set; }
    }
}
