using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetAll();
        Task<IEnumerable<Course>> GetByStatus(CourseStatus status);
        Task EditDetails(Guid courseId, CourseDetails details);
        Task<Course?> GetById(Guid id);
        Task EditCourseStatus(Guid courseId, CourseStatus status);
        Task Edit(Course course);
        Task<int> GetElementsCount(Guid courseId);
    }
}
