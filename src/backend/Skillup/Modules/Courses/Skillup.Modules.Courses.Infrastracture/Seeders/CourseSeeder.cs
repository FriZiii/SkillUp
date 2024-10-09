using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using Skillup.Shared.Abstractions.Seeder;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseSeeder : ISeeder
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
            if (!await _categories.AnyAsync() && !await _subcategories.AnyAsync())
            {
                await SeedCategories(_categories, _subcategories);
            }
            if(!await _courses.AnyAsync())
            {
                await SeedCourses(_categories, _subcategories);
            }
        }

        private async Task SeedCategories(DbSet<Category> categories, DbSet<Subcategory> subcategories)
        {
            var categoriesToAdd = new List<Category>();
            var subcategoriesToAdd = new List<Subcategory>();
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

                categories.Add(category);

                foreach (var subCategoryName in categoryWithSubcategory.Value)
                {
                    var subCategory = new Subcategory()
                    {
                        Name = subCategoryName,
                        CategoryId = category.Id
                    };

                    subcategoriesToAdd.Add(subCategory);
                }
            }
            await subcategories.AddRangeAsync(subcategoriesToAdd);
            await _context.SaveChangesAsync();
        }

        private async Task SeedCourses(DbSet<Category> categories, DbSet<Subcategory> subcategories)
        {
            var coursesToAdd = new List<Course>
                {
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "C# from basics",
                            Subtitle = "Learn hot to write applications! Programming in practise!",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Programming"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Basics of programming"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "We invite both current programmers who want to gain a deep understanding of C# and individuals who have had no prior experience with programming but wish to learn what programming is and how to create applications.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Program applications in C# language", "A practical approach to programming", "What all this programming is about, in C# and beyond" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Knowledge of English at a basic level"}),
                            IntendedFor = new StringListValueObject(new List<string> { "People who want to learn C# programming", "Beginner programmers who want to learn or improve on the C# language" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to English",
                            Subtitle = "Learn English from scratch!",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Languages"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "English"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "Perfect for those new to the English language. Start learning English with practical examples.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand basic English", "Learn practical phrases", "Communicate confidently in English" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "No prior knowledge required" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Anyone new to English", "People interested in learning English" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Web Development for Beginners",
                            Subtitle = "Create your first website!",
                        },

                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Programming"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Web development"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "This course is designed for beginners who want to learn how to build websites using modern web technologies.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Build responsive websites", "Learn HTML, CSS, and JavaScript basics", "Understand how websites work" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic computer skills" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Beginner web developers", "People interested in web design" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Project Management Basics",
                            Subtitle = "Master project management techniques",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Business and managing"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Project management"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "This course will help you understand project management fundamentals, perfect for aspiring project managers.",
                            Level = CourseLevel.Intermediate,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand project life cycle", "Master task planning and execution", "Improve team management skills" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic business understanding" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Aspiring project managers", "Professionals in project-based environments" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to Algebra",
                            Subtitle = "Algebra for beginners",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Mathematics"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Algebra"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "A foundational course in algebra designed for students or professionals looking to brush up on their algebra skills.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand algebraic expressions", "Solve linear equations", "Work with polynomials" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic arithmetic" }),
                            IntendedFor = new StringListValueObject(new List<string> { "High school students", "Professionals who need algebra for work" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Creative Writing 101",
                            Subtitle = "Unlock your inner writer",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Art"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Literature"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "This course will help you find your voice and express your thoughts through creative writing.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Develop storytelling skills", "Master creative writing techniques", "Craft compelling characters and plots" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic understanding of English" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Aspiring writers", "Anyone with an interest in writing fiction or non-fiction" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "German for Beginners",
                            Subtitle = "Start speaking German today!",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Languages"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "German"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "An introductory course to the German language, perfect for beginners wanting to learn basic phrases and vocabulary.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Learn basic German phrases", "Understand everyday vocabulary", "Start simple conversations in German" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "No prior knowledge required" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Beginners in the German language", "Travelers to German-speaking countries" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Game Development with Unity",
                            Subtitle = "Build your first game!",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Programming"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Game development"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "Learn how to develop video games using the Unity game engine in this beginner-level course.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand the Unity interface", "Create simple 2D and 3D games", "Learn scripting in C#" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic computer skills" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Aspiring game developers", "People interested in creating their own games" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Motivation and Self-Discipline",
                            Subtitle = "Achieve your goals through self-motivation",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Self-development"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Motivation"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "Learn how to stay motivated and disciplined in both your personal and professional life.",
                            Level = CourseLevel.Intermediate,
                             ObjectivesSummary = new StringListValueObject(new List<string> { "Develop long-term motivation", "Learn techniques for self-discipline", "Achieve personal and career goals" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Willingness to improve oneself" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Individuals looking to boost motivation", "Anyone who struggles with staying disciplined" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Finance for Non-Finance Professionals",
                            Subtitle = "Understand financial concepts quickly",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Business and managing"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Finance"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "This course simplifies finance for professionals without a financial background, helping them understand key concepts and make better decisions.",
                            Level = CourseLevel.Intermediate,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand financial statements", "Learn about key financial ratios", "Make informed financial decisions" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic math skills" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Non-finance professionals", "Business owners looking to understand finance better" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Introduction to Biology",
                            Subtitle = "Explore the world of living organisms",
                        },

                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Science"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Biology"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "A beginner's guide to understanding the basics of biology, from cells to ecosystems.",
                            Level = CourseLevel.Beginner,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Understand the structure of cells", "Learn about different ecosystems", "Study the basics of human anatomy" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Basic interest in life sciences" }),
                            IntendedFor = new StringListValueObject(new List<string> { "High school students", "Anyone interested in biology" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    },
                    new Course()
                    {
                        Info = new CourseInfo()
                        {
                            Title = "Creative Thinking and Innovation",
                            Subtitle = "Unleash your creative potential",
                        },
                        CategoryId = (await categories.FirstOrDefaultAsync(c => c.Name == "Self-development"))!.Id,
                        SubcategoryId = (await subcategories.FirstOrDefaultAsync(c => c.Name == "Creativity & hobbies"))!.Id,
                        Details = new CourseDetails()
                        {
                            Description = "A course designed to enhance your creativity and teach you how to think innovatively in both personal and professional scenarios.",
                            Level = CourseLevel.ForEveryone,
                            ObjectivesSummary = new StringListValueObject(new List<string> { "Develop innovative thinking", "Boost creativity in everyday life", "Learn techniques for brainstorming" }),
                            MustKnowBefore = new StringListValueObject(new List<string> { "Open mind and curiosity" }),
                            IntendedFor = new StringListValueObject(new List<string> { "Creative professionals", "Anyone wanting to improve creative skills" }),
                        },
                        ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
                    }
                };
            await _courses.AddRangeAsync(coursesToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
