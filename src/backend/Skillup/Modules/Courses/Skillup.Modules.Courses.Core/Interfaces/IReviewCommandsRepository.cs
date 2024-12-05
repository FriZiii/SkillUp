using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IReviewCommentRepository
    {
        Task Add(ReviewComment comment);
        Task Delete(Guid commentId);
        Task<ReviewComment?> Get(Guid commentId);
        Task Update(ReviewComment comment);
    }
}
