using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetByStatus(CourseStatus status);
        Task EditDetails(Guid courseId, CourseDetails details);
        Task<Course?> GetById(Guid id);
        Task Publish(Guid courseId);
        Task Edit(Course course);
    }
}
