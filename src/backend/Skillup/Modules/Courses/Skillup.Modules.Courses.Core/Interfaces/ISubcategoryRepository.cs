using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task Add(Subcategory subcategory);
        Task<IEnumerable<Subcategory>> GetAll();
    }
}
