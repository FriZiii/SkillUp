using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IAssetsRepository
    {
        Task AddArticle(Article article);
        Task AddVideo(Video video);
        Task Delete(Guid id, AssetType type);
        Task<Asset?> Get(Guid id, AssetType type);
    }
}
