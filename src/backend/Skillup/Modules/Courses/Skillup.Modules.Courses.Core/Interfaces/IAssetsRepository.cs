using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IAssetsRepository
    {
        Task AddArticle(Article article);
        Task AddVideo(Video video);
        Task AddAssignment(Assignment assignment);
        Task Delete(Guid id, AssetType type);
        Task<Asset?> GetByElementId(Guid elementId);
    }
}
