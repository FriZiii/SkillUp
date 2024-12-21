using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByElementId(Guid elementId);
        Task Add(Comment comment);
        Task Delete(Guid commentId);
        Task ToggleLikeComment(Guid commentId, Guid userId);
    }
}
