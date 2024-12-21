using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Comments
{
    public record ToggleLikeForCommentRequest(Guid CommentId, Guid UserId) : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }
    }
}
