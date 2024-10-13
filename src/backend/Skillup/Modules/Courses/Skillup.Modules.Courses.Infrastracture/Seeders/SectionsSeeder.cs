using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class SectionsSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly DbSet<Section> _sections;
        private List<Course> _courseList = new();

        public SectionsSeeder(CoursesDbContext context)
        {
            _context = context;
            _courses = context.Courses;
            _sections = context.Sections;
        }
        public async Task Seed()
        {
            if (!await _sections.AnyAsync())
            {
                _courseList = await _courses.ToListAsync();
                await _sections.AddRangeAsync(CreateSections());
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Section> CreateSections()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "course-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var sections = new List<Section>();
            var courseData = JsonSerializer.Deserialize<List<CourseJsonModel>>(jsonString, options);

            foreach (var course in courseData!)
            {
                foreach (var section in course.Sections)
                {
                    sections.Add(new Section()
                    {
                        Title = section.Title,
                        CourseId = _courseList.First(x => x.Title == course.Title).Id
                    });
                }
            }

            return sections;
        }

    }
}
