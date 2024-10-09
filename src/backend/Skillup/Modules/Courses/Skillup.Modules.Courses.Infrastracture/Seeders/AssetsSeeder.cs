using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class AssetsSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Element> _elements;
        private readonly DbSet<Article> _articles;
        private readonly DbSet<Video> _videos;
        private readonly DbSet<Assignment> _assignments;

        public AssetsSeeder(CoursesDbContext context)
        {
            _context = context;
            _elements = context.Elements;
            _articles = context.Articles;
            _videos = context.Videos;
            _assignments = context.Assignments;
            _articles = context.Articles;
            _videos = context.Videos;
        }

        public async Task Seed()
        {
            var articlesToAdd = new List<Article>()
            {
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "What is .NET?")).Id,
                    HTMLContent = ".NET is...."
                },
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Variables and Constants in C#")).Id,
                    HTMLContent = "Variables...."
                },
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Data Types (int, string, double, etc.)")).Id,
                    HTMLContent = "Data Types...."
                },
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "If-Else Statements")).Id,
                    HTMLContent = "...."
                },
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "For / While loop")).Id,
                    HTMLContent = "...."
                },
                new Article()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Nouns and Pronouns")).Id,
                    HTMLContent = "...."
                },
            };
            var videosToAdd = new List<Video>()
            {
                new Video()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Installing .NET SDK")).Id,
                    Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                },
                new Video()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Setting up Visual Studio")).Id,
                    Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                },
                new Video()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "First C# Program: \"Hello World\"")).Id,
                    Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                },
                new Video()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Numbers and Dates")).Id,
                    Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                },
            };
            var assignmentsToAdd = new List<Assignment>()
            {
                new Assignment()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Everyday Vocabulary: Food, Weather, Shopping")).Id,
                    Instruction = "Do something"
                },
                new Assignment()
                {
                    ElementId = (await _elements.FirstOrDefaultAsync(e => e.Title == "Asking and Answering Questions")).Id,
                    Instruction = "Do something"
                },
            };
            await _articles.AddRangeAsync(articlesToAdd);
            await _videos.AddRangeAsync(videosToAdd);
            await _assignments.AddRangeAsync(assignmentsToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
