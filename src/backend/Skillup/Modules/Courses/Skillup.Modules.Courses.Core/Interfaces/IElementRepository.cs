using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IElementRepository
    {
        Task AddElement(Element element);
        Task AddAssignment(Element element);
        Task<Element> GetById(Guid elementId);
        Task<List<Element>> GetElementsBySectionId(Guid sectionId);
        Task Edit(Element element);
        Task EditIndexes(IEnumerable<Element> elements);
        Task Delete(Guid elemenId);
    }
}
