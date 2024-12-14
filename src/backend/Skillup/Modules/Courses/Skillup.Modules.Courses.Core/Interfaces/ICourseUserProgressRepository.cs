using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseUserProgressRepository
    {
        Task<CourseUserProgess?> GetByUserAndElementId(Guid userId, Guid elementId);
        Task Add(CourseUserProgess userProgess);
        Task Delete(Guid id);

        Task<IEnumerable<CourseUserProgess>> GetByUserId(Guid userId);
        Task<IEnumerable<CourseUserProgess>> GetByCourseAndUserId(Guid courseId, Guid userId);
    }
}
