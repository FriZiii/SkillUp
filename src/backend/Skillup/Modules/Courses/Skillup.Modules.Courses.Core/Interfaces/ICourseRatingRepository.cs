using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseRatingRepository
    {
        Task Add(CourseRating courseRating);
        Task Update(CourseRating courseRating);
        Task<IEnumerable<CourseRating>> GetByUserId(Guid userId);
        Task<IEnumerable<CourseRating>> GetByCourseId(Guid courseId);
        Task<IEnumerable<CourseRating>> Get();
        Task<CourseRating?> GetById(Guid id);
        Task Delete(Guid id);
    }
}
