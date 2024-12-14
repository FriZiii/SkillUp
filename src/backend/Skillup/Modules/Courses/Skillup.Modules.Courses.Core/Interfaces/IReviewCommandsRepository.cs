using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IReviewCommentRepository
    {
        Task Add(CourseReviewComment comment);
        Task Delete(Guid commentId);
        Task<CourseReviewComment?> Get(Guid commentId);
        Task Update(CourseReviewComment comment);
    }
}
