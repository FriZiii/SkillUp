using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class AssetsRepository : IAssetsRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Video> _videos;
        private readonly DbSet<Article> _articles;
        private readonly DbSet<Assignment> _assignemnts;

        public AssetsRepository(CoursesDbContext context)
        {
            _context = context;
            _videos = _context.Videos;
            _articles = _context.Articles;
            _assignemnts = _context.Assignments;
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

        public async Task AddAssignment(Assignment assignment)
        {
            await _assignemnts.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task EditAssignment(Assignment assignment)
        {
            var assignmentToEdit = await _assignemnts.FirstOrDefaultAsync(s => s.Id == assignment.Id) ?? throw new NotFoundException($"Assignment with ID {assignment.Id} not found");

            assignmentToEdit.Instruction = assignment.Instruction;

            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id, AssetType type)
        {
            switch (type)
            {
                case AssetType.Article:
                    var articleToRemove = await _articles.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"Article with ID {id} not found");
                    _articles.Remove(articleToRemove);
                    break;

                case AssetType.Video:
                    var videoToRemove = await _videos.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"Video with ID {id} not found");
                    _videos.Remove(videoToRemove);
                    break;

                case AssetType.Exercise:
                    var assignmentToRemove = await _assignemnts.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"Exercise with ID {id} not found");
                    _assignemnts.Remove(assignmentToRemove);
                    break;

                default:
                    throw new BadRequestException("Wrong asset type provided");
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Asset?> GetByElementId(Guid elementId)
        {
            var element = await _context.Elements.FirstOrDefaultAsync(x => x.Id == elementId) ?? throw new NotFoundException($"Element with ID {elementId} not found");

            return element.AssetType switch
            {
                AssetType.Article => await _articles.FirstOrDefaultAsync(x => x.ElementId == element.Id),
                AssetType.Video => await _videos.FirstOrDefaultAsync(x => x.ElementId == element.Id),
                AssetType.Exercise => await _assignemnts.FirstOrDefaultAsync(x => x.ElementId == element.Id),
                _ => null,
            };
        }

        public async Task<Assignment> GetAssignmentById(Guid id)
        {
            var assignment = await _assignemnts.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"Assignment with ID {id} not found");
            return assignment;
        }
    }
}
