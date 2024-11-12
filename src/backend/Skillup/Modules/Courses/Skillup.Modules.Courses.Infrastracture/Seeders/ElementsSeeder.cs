using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class ElementsSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Section> _sections;
        private readonly DbSet<Element> _elements;
        private List<Section> _sectionsList = new();

        public ElementsSeeder(CoursesDbContext context)
        {
            _context = context;
            _sections = context.Sections;
            _elements = context.Elements;
        }
        public async Task Seed()
        {
            if (!await _elements.AnyAsync())
            {
                _sectionsList = await _sections.ToListAsync();
                await _elements.AddRangeAsync(CreateArticleElements());
                await _elements.AddRangeAsync(CreateVideosElements());
                await _elements.AddRangeAsync(CreateAssignmentsElements());
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Element> CreateArticleElements()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "article-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<ArticleElementJsonModel>>(jsonString, options);

            return data!.Select(CreateArticleElement);
        }

        private IEnumerable<Element> CreateVideosElements()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "video-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<VideoElementJsonModel>>(jsonString, options);

            return data!.Select(CreateVideoElement);
        }

        private IEnumerable<Element> CreateAssignmentsElements()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "assignment-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<AssignmentElementJsonModel>>(jsonString, options);

            return data!.Select(CreateAssignmentElement);
        }

        private Element CreateArticleElement(ArticleElementJsonModel jsonModel)
        {
            return new Element()
            {
                Title = jsonModel.Title,
                Description = jsonModel.Description,
                Type = Enum.Parse<AssetType>(jsonModel.Type),
                Index = jsonModel.Index,
                IsFree = jsonModel.IsFree,
                SectionId = _sectionsList.First(x => x.Title == jsonModel.SectionTitle).Id,
                //Asset = new Article()
                //{
                //    HTMLContent = jsonModel.Article.HTMLContent
                //}
            };
        }

        private Element CreateVideoElement(VideoElementJsonModel jsonModel)
        {
            return new Element()
            {
                Title = jsonModel.Title,
                Description = jsonModel.Description,
                Type = Enum.Parse<AssetType>(jsonModel.Type),
                Index = jsonModel.Index,
                IsFree = jsonModel.IsFree,
                SectionId = _sectionsList.First(x => x.Title == jsonModel.SectionTitle).Id,
                //Asset = new Video()
                //{
                //    Url = jsonModel.Video.Url
                //}
            };
        }

        private Element CreateAssignmentElement(AssignmentElementJsonModel jsonModel)
        {
            return new Element()
            {
                Title = jsonModel.Title,
                Description = jsonModel.Description,
                Type = Enum.Parse<AssetType>(jsonModel.Type),
                Index = jsonModel.Index,
                IsFree = jsonModel.IsFree,
                SectionId = _sectionsList.First(x => x.Title == jsonModel.SectionTitle).Id,
                Asset = new Assignment()
                {
                    Instruction = jsonModel.Assignment.Instruction
                }
            };
        }

    }
}
