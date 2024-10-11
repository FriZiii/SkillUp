using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task Add(Subcategory subcategory);
        Task<IEnumerable<Subcategory>> GetAll();
        Task<Subcategory> GetById(Guid id);
    }
}
