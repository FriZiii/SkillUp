using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseReviewRepository
    {
        Task Add(CourseReview courseReview);
        Task<CourseReview?> Get(Guid reviewId);
        Task<IEnumerable<CourseReview>> GetByCourse(Guid courseId);
        Task<CourseReview?> GetLatestByCourse(Guid courseId);
        Task<IEnumerable<CourseReview>> GetWithStatus(ReviewStatus status);
        Task Update(CourseReview review);
    }
}
