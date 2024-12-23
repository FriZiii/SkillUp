using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCommentsByElementIdRequest(Guid ElementId, Guid UserId) : IRequest<IEnumerable<CommentDto>>;
}
