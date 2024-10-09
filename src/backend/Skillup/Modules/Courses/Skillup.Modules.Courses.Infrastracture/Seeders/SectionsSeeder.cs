using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class SectionsSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly DbSet<Section> _sections;

        public SectionsSeeder(CoursesDbContext context)
        {
            _context = context;
            _courses = context.Courses;
            _sections = context.Sections;
        }
        public async Task Seed()
        {
            var course1 = await _courses.FirstOrDefaultAsync(c => c.Info.Title == "C# from basics");
            var course2 = await _courses.FirstOrDefaultAsync(c => c.Info.Title == "Introduction to English");
            var course3 = await _courses.FirstOrDefaultAsync(c => c.Info.Title == "Web Development for Beginners");

            var sectionsToAdd = new List<Section>()
            {
                new Section()
                {
                    Title = "Introduction to C# and .NET Framework",
                    CourseId = course1.Id
                },
                new Section()
                {
                    Title = "Basic Syntax and Data Types",
                    CourseId = course1.Id
                },
                new Section()
                {
                    Title = "Control Structures and Loops",
                    CourseId = course1.Id
                },
                new Section()
                {
                    Title = "Working with Classes and Objects",
                    CourseId = course1.Id
                },
                new Section()
                {
                    Title = "Error Handling and Debugging",
                    CourseId = course1.Id
                },
                new Section()
                {
                    Title = "Basic Grammar and Sentence Structure",
                    CourseId = course2.Id
                },
                new Section()
                {
                    Title = "Common Vocabulary and Phrases",
                    CourseId = course2.Id
                },
                new Section()
                {
                    Title = "Conversational English",
                    CourseId = course2.Id
                },
                new Section()
                {
                    Title = "Reading and Writing in English",
                    CourseId = course2.Id
                },
                new Section()
                {
                    Title = "Listening and Comprehension Skills",
                    CourseId = course2.Id
                },
                new Section()
                {
                    Title = "Introduction to Web Technologies",
                    CourseId = course3.Id
                },
                new Section()
                {
                    Title = "HTML Basics and Structure",
                    CourseId = course3.Id
                },
                new Section()
                {
                    Title = "CSS for Styling Web Pages",
                    CourseId = course3.Id
                },
                new Section()
                {
                    Title = "Introduction to JavaScript",
                    CourseId = course3.Id
                },
                new Section()
                {
                    Title = "Building a Simple Website",
                    CourseId = course3.Id
                }
            };
            _sections.AddRange(sectionsToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
