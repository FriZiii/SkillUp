using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ISectionRepository
    {
        Task Add(Section section);
        Task<List<Section>> GetSectionsByCourseId(Guid courseId);
        Task<Section> GetById(Guid sectionId);
        Task Edit(Section section);
        Task EditMultiple(IEnumerable<Section> sections);
        Task Delete(Section section);
    }
}
