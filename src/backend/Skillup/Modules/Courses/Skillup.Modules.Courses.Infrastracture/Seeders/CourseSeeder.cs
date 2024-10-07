using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Category> _categories;
        private readonly DbSet<Subcategory> _subcategories;
        private readonly DbSet<Course> _courses;

        public CourseSeeder(CoursesDbContext context)
        {
            _context = context;
            _categories = context.Categories;
            _subcategories = context.Subcategories;
            _courses = context.Courses;
        }
        public async Task Seed()
        {
            if(!await _categories.AnyAsync())
            {
                await SeedCategories(_categories, _subcategories);

                var coursesToAdd = new List<Course>
                {
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "C# from basics",
                            Subtitle = "Learn hot to write applications! Programming in practise!",
                            Description = "We invite both current programmers who want to gain a deep understanding of C# and individuals who have had no prior experience with programming but wish to learn what programming is and how to create applications."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Programming"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Basics of programming"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Program applications in C# language", "A practical approach to programming", "What all this programming is about, in C# and beyond" }),
                        MustKnowBefore = new StringList(new List<string> { "Knowledge of English at a basic level"}),
                        IntendedFor = new StringList(new List<string> { "People who want to learn C# programming", "Beginner programmers who want to learn or improve on the C# language" }),
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                        Price = new Price(50),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to English",
                            Subtitle = "Learn English from scratch!",
                            Description = "Perfect for those new to the English language. Start learning English with practical examples."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Languages"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "English"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Understand basic English", "Learn practical phrases", "Communicate confidently in English" }),
                        MustKnowBefore = new StringList(new List<string> { "No prior knowledge required" }),
                        IntendedFor = new StringList(new List<string> { "Anyone new to English", "People interested in learning English" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(30),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Web Development for Beginners",
                            Subtitle = "Create your first website!",
                            Description = "This course is designed for beginners who want to learn how to build websites using modern web technologies."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Programming"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Web development"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Build responsive websites", "Learn HTML, CSS, and JavaScript basics", "Understand how websites work" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic computer skills" }),
                        IntendedFor = new StringList(new List<string> { "Beginner web developers", "People interested in web design" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(40),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Project Management Basics",
                            Subtitle = "Master project management techniques",
                            Description = "This course will help you understand project management fundamentals, perfect for aspiring project managers."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Business and managing"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Project management"),
                        Level = CourseLevel.Intermediate,
                        ObjectivesSummary = new StringList(new List<string> { "Understand project life cycle", "Master task planning and execution", "Improve team management skills" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic business understanding" }),
                        IntendedFor = new StringList(new List<string> { "Aspiring project managers", "Professionals in project-based environments" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(60),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to Algebra",
                            Subtitle = "Algebra for beginners",
                            Description = "A foundational course in algebra designed for students or professionals looking to brush up on their algebra skills."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Mathematics"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Algebra"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Understand algebraic expressions", "Solve linear equations", "Work with polynomials" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic arithmetic" }),
                        IntendedFor = new StringList(new List<string> { "High school students", "Professionals who need algebra for work" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(45),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Creative Writing 101",
                            Subtitle = "Unlock your inner writer",
                            Description = "This course will help you find your voice and express your thoughts through creative writing."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Art"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Literature"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Develop storytelling skills", "Master creative writing techniques", "Craft compelling characters and plots" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic understanding of English" }),
                        IntendedFor = new StringList(new List<string> { "Aspiring writers", "Anyone with an interest in writing fiction or non-fiction" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(35),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "German for Beginners",
                            Subtitle = "Start speaking German today!",
                            Description = "An introductory course to the German language, perfect for beginners wanting to learn basic phrases and vocabulary."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Languages"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "German"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Learn basic German phrases", "Understand everyday vocabulary", "Start simple conversations in German" }),
                        MustKnowBefore = new StringList(new List<string> { "No prior knowledge required" }),
                        IntendedFor = new StringList(new List<string> { "Beginners in the German language", "Travelers to German-speaking countries" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(30),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Game Development with Unity",
                            Subtitle = "Build your first game!",
                            Description = "Learn how to develop video games using the Unity game engine in this beginner-level course."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Programming"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Game development"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Understand the Unity interface", "Create simple 2D and 3D games", "Learn scripting in C#" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic computer skills" }),
                        IntendedFor = new StringList(new List<string> { "Aspiring game developers", "People interested in creating their own games" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(60),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Motivation and Self-Discipline",
                            Subtitle = "Achieve your goals through self-motivation",
                            Description = "Learn how to stay motivated and disciplined in both your personal and professional life."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Self-development"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Motivation"),
                        Level = CourseLevel.Intermediate,
                        ObjectivesSummary = new StringList(new List<string> { "Develop long-term motivation", "Learn techniques for self-discipline", "Achieve personal and career goals" }),
                        MustKnowBefore = new StringList(new List<string> { "Willingness to improve oneself" }),
                        IntendedFor = new StringList(new List<string> { "Individuals looking to boost motivation", "Anyone who struggles with staying disciplined" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(50),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Finance for Non-Finance Professionals",
                            Subtitle = "Understand financial concepts quickly",
                            Description = "This course simplifies finance for professionals without a financial background, helping them understand key concepts and make better decisions."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Business and managing"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Finance"),
                        Level = CourseLevel.Intermediate,
                        ObjectivesSummary = new StringList(new List<string> { "Understand financial statements", "Learn about key financial ratios", "Make informed financial decisions" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic math skills" }),
                        IntendedFor = new StringList(new List<string> { "Non-finance professionals", "Business owners looking to understand finance better" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(70),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to Biology",
                            Subtitle = "Explore the world of living organisms",
                            Description = "A beginner's guide to understanding the basics of biology, from cells to ecosystems."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Science"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Biology"),
                        Level = CourseLevel.Beginner,
                        ObjectivesSummary = new StringList(new List<string> { "Understand the structure of cells", "Learn about different ecosystems", "Study the basics of human anatomy" }),
                        MustKnowBefore = new StringList(new List<string> { "Basic interest in life sciences" }),
                        IntendedFor = new StringList(new List<string> { "High school students", "Anyone interested in biology" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(40),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Creative Thinking and Innovation",
                            Subtitle = "Unleash your creative potential",
                            Description = "A course designed to enhance your creativity and teach you how to think innovatively in both personal and professional scenarios."
                        },
                        Category = await _categories.SingleOrDefaultAsync(c => c.Name == "Self-development"),
                        Subcategory = await _subcategories.SingleOrDefaultAsync(c => c.Name == "Creativity & hobbies"),
                        Level = CourseLevel.AllLevels,
                        ObjectivesSummary = new StringList(new List<string> { "Develop innovative thinking", "Boost creativity in everyday life", "Learn techniques for brainstorming" }),
                        MustKnowBefore = new StringList(new List<string> { "Open mind and curiosity" }),
                        IntendedFor = new StringList(new List<string> { "Creative professionals", "Anyone wanting to improve creative skills" }),
                        ThumbnailUrl = new Uri("url"),
                        Price = new Price(50),
                    }
                };
                foreach (var course in coursesToAdd)
                {
                    await _courses.AddAsync(course);
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedCategories(DbSet<Category> categories, DbSet<Subcategory> subcategories)
        {
            var categoriesWithSubcategories = new Dictionary<string, List<string>>()
            {
                { "Languages", new List<string> { "English", "German", "Spanish", "French", "Other" } },
                { "Programming", new List<string> { "Basics of programming", "Game development", "Web development", "Mobile development", "Databases", "Artificial intelligence", "Cybersecurity", "Other" } },
                { "Self-development", new List<string> { "Professional development", "Motivation", "Health", "Sports", "Creativity & hobbies", "Other" } },
                { "Business and managing", new List<string> { "Project management", "Marketing", "Finance", "HR & recruitment", "Other" } },
                { "Science", new List<string> { "Mathematics", "Physics", "Biology", "Chemistry", "Engineering", "Other" } },
                { "Mathematics", new List<string> { "Algebra", "Analysis", "Statistics", "Probability", "Discrete mathematics" } },
                { "Humanities", new List<string> { "Philosophy", "History", "Art history", "Literature", "Culture", "Religions", "Law", "Other" } },
                { "Art", new List<string> { "Literature", "Music", "Visual arts", "Other" } },
                { "Teaching", new List<string> { "Teaching methods", "Online teaching", "Teaching children & teenagers", "Other" } },
                { "Applications", new List<string> { "Microsoft", "Apple", "Autodesk", "Adobe", "Google", "Other" } }
            };

            foreach (var categoryWithSubcategory in categoriesWithSubcategories)
            {
                var category = new Category()
                {
                    Name = categoryWithSubcategory.Key
                };

                await categories.AddAsync(category);

                foreach (var subCategoryName in categoryWithSubcategory.Value)
                {
                    var subCategory = new Subcategory()
                    {
                        Name = subCategoryName,
                        //CategoryId = category.Id // Assuming the relation uses CategoryId
                    };

                    await subcategories.AddAsync(subCategory);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
