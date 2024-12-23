using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Comments
{
    public record AddCommentRequest : IRequest
    {
        [JsonIgnore]
        public Guid ElementId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }

        public string Content { get; set; }
        public Guid? ParentCommentId { get; set; }
    }
}
