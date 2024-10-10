using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class ElementsSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Section> _sections;
        private readonly DbSet<Element> _elements;

        public ElementsSeeder(CoursesDbContext context)
        {
            _context = context;
            _sections = context.Sections;
            _elements = context.Elements;
        }
        public async Task Seed()
        {
            var elementsToAdd = new List<Element>()
            {
                new Element()
                {
                    Title = "What is .NET?",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Introduction to C# and .NET Framework")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = ".NET is...."
                    },
                },
                new Element()
                {
                    Title = "Installing .NET SDK",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Introduction to C# and .NET Framework")).Id,
                    Asset = new Video()
                    {
                        Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                    },
                },
                new Element()
                {
                    Title = "Setting up Visual Studio",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Introduction to C# and .NET Framework")).Id,
                    Asset = new Video()
                    {
                        Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                    },
                },
                new Element()
                {
                    Title = "First C# Program: \"Hello World\"",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Introduction to C# and .NET Framework")).Id,
                    Asset = new Video()
                    {
                        Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                    },
                },
                new Element()
                {
                    Title = "Variables and Constants in C#",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Basic Syntax and Data Types")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = "Variables...."
                    },
                },
                new Element()
                {
                    Title = "Data Types (int, string, double, etc.)",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Basic Syntax and Data Types")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = "Data Types...."
                    },
                },
                new Element()
                {
                    Title = "If-Else Statements",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Control Structures and Loops")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = "...."
                    },
                },
                new Element()
                {
                    Title = "For / While loop",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Control Structures and Loops")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = "...."
                    },
                },
                new Element()
                {
                    Title = "Nouns and Pronouns",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Basic Grammar and Sentence Structure")).Id,
                    Asset = new Article()
                    {
                        HTMLContent = "...."
                    },
                },
                new Element()
                {
                    Title = "Everyday Vocabulary: Food, Weather, Shopping",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Common Vocabulary and Phrases")).Id,
                    Asset = new Assignment()
                    {
                        Instruction = "Do something"
                    },
                },
                new Element()
                {
                    Title = "Numbers and Dates",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Basic Grammar and Sentence Structure")).Id,
                    Asset = new Video()
                    {
                        Url = new Uri("https://www.youtube.com/watch?v=md6YepdRfoA&ab_channel=afilmbykirk")
                    },
                },
                new Element()
                {
                    Title = "Practising Present Simple",
                    IsFree = true,
                    IsPublished = true,
                    SectionId = (await _sections.FirstOrDefaultAsync(s => s.Title == "Conversational English")).Id,
                    Asset = new Assignment()
                    {
                        Instruction = "Use a proper verb form"
                    },
                },
            };

            await _elements.AddRangeAsync(elementsToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
