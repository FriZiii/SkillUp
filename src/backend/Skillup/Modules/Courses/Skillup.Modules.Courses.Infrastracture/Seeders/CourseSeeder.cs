using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseSeeder : ISeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly IClock _clock;
        private readonly CategorySeeder _categoriesSeeder;
        private readonly SectionsSeeder _sectionsSeeder;
        private readonly ElementsSeeder _elementsSeeder;
        private readonly ExerciseSeeder _exerciseSeeder;

        private List<Category> _categories;
        private List<Subcategory> _subCategories;

        public CourseSeeder(CoursesDbContext context, IClock clock)
        {
            _context = context;
            _courses = context.Courses;
            _clock = clock;
            _categoriesSeeder = new CategorySeeder(context);
            _sectionsSeeder = new SectionsSeeder(context);
            _elementsSeeder = new ElementsSeeder(context);
            _exerciseSeeder = new ExerciseSeeder(context);
        }
        public async Task Seed()
        {
            _categories = await _context.Categories.ToListAsync();
            _subCategories = await _context.Subcategories.ToListAsync();

            if (!await _context.Categories.AnyAsync() && !await _context.Subcategories.AnyAsync())
            {
                await _categoriesSeeder.Seed();
            }

            if (!await _courses.AnyAsync())
            {
                await SeedCourses();
            }

            if (!await _context.Sections.AnyAsync())
            {
                await _sectionsSeeder.Seed();
            }

            if (!await _context.Elements.AnyAsync())
            {
                await _elementsSeeder.Seed();
            }

            if (!await _context.QuizQuestionExercises.AnyAsync() && !await _context.QuestionAnswerExercises.AnyAsync() && !await _context.QuestionAnswerExercises.AnyAsync())
            {
                await _exerciseSeeder.Seed();
            }
        }

        private async Task SeedCourses()
        {
            await _courses.AddRangeAsync(CreateCourses());
            await _context.SaveChangesAsync();
        }

        private List<Course> CreateCourses()
        {
            return
            [
                CreateCourse("C# from basics", "Programming", "Basics of programming",
                    CreateCourseDetails(
                        "Learn hot to write applications! Programming in practise!",
                        "We invite both current programmers who want to gain a deep understanding of C# and individuals who have had no prior experience with programming but wish to learn what programming is and how to create applications.",
                        CourseLevel.Beginner,
                        ["Program applications in C# language", "A practical approach to programming", "What all this programming is about, in C# and beyond"],
                        ["Knowledge of English at a basic level"],
                        ["People who want to learn C# programming", "Beginner programmers who want to learn or improve on the C# language"],
                        new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"))),

                CreateCourse("Introduction to English", "Languages", "English",
                    CreateCourseDetails(
                        "Learn English from scratch!",
                        "Perfect for those new to the English language. Start learning English with practical examples.",
                        CourseLevel.Beginner,
                        ["Understand basic English", "Learn practical phrases", "Communicate confidently in English"],
                        ["No prior knowledge required"],
                        ["Anyone new to English", "People interested in learning English"],
                        new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg")))
            ];
        }

        private Course CreateCourse(string title, string categoryName, string subcategoryName, CourseDetails details)
        {
            return new Course()
            {
                Title = title,
                CategoryId = _categories.First(x => x.Name == categoryName).Id,
                SubcategoryId = _subCategories.First(x => x.Name == subcategoryName).Id,
                Details = details
            };
        }

        private CourseDetails CreateCourseDetails(
            string subtitle, string description, CourseLevel level,
            List<string> objectivesSummary, List<string> mustKnowBefore, List<string> intendedFor,
            Uri thumbnailUrl)
        {
            return new CourseDetails()
            {
                Subtitle = subtitle,
                Description = description,
                Level = level,
                ObjectivesSummary = new StringListValueObject(objectivesSummary),
                MustKnowBefore = new StringListValueObject(mustKnowBefore),
                IntendedFor = new StringListValueObject(intendedFor),
                ThumbnailUrl = thumbnailUrl,
            };
        }
    }
}
