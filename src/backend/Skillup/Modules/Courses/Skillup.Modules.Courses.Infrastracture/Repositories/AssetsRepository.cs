using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class AssetsRepository : IAssetsRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Video> _videos;
        private readonly DbSet<Article> _articles;

        public AssetsRepository(CoursesDbContext context)
        {
            _context = context;
            _videos = _context.Videos;
            _articles = _context.Articles;
        }

        public async Task AddVideo(Video video)
        {
            await _videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task AddArticle(Article article)
        {
            await _articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id, AssetType type)
        {
            switch (type)
            {
                case AssetType.Article:
                    var articleToRemove = await _articles.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception(); // Article doesnt exist  //TODO: Custom ex
                    _articles.Remove(articleToRemove);
                    break;

                case AssetType.Video:
                    var videoToRemove = await _videos.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception(); // Video doesnt exist  //TODO: Custom ex
                    _videos.Remove(videoToRemove);
                    break;

                case AssetType.Exercise:
                    //TODO
                    break;

                default:
                    throw new Exception(); // Wrong asset type  //TODO: Custom ex
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Asset?> GetByElementId(Guid elementId)
        {
            var element = await _context.Elements.FirstOrDefaultAsync(x => x.Id == elementId) ?? throw new Exception(); // TODO: custome ex

            return element.AssetType switch
            {
                AssetType.Article => await _articles.FirstOrDefaultAsync(x => x.ElementId == element.Id),
                AssetType.Video => await _videos.FirstOrDefaultAsync(x => x.ElementId == element.Id),
                AssetType.Exercise => null,
                _ => null,
            };
        }
    }
}
