using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetAll();
    }
}
