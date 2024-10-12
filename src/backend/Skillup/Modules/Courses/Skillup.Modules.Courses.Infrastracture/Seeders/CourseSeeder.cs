using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseSeeder : ISeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly IClock _clock;

        private List<Category> _categories;
        private List<Subcategory> _subCategories;

        public CourseSeeder(CoursesDbContext context, IClock clock)
        {
            _context = context;
            _courses = context.Courses;
            _clock = clock;
        }

        public async Task Seed()
        {
            if (!await _context.Categories.AnyAsync() && !await _context.Subcategories.AnyAsync())
            {
                var _categoriesSeeder = new CategorySeeder(_context);
                await _categoriesSeeder.Seed();
            }

            _categories = await _context.Categories.ToListAsync();
            _subCategories = await _context.Subcategories.ToListAsync();

            if (!await _courses.AnyAsync())
            {
                await SeedCourses();
            }

            if (!await _context.Sections.AnyAsync())
            {
                var _sectionsSeeder = new SectionsSeeder(_context);
                await _sectionsSeeder.Seed();
            }

            if (!await _context.Elements.AnyAsync())
            {
                var _elementsSeeder = new ElementsSeeder(_context);
                await _elementsSeeder.Seed();
            }

            if (!await _context.QuizQuestionExercises.AnyAsync() && !await _context.QuestionAnswerExercises.AnyAsync() && !await _context.QuestionAnswerExercises.AnyAsync())
            {
                var _exerciseSeeder = new ExerciseSeeder(_context);
                await _exerciseSeeder.Seed();
            }
        }

        private async Task SeedCourses()
        {
            await _courses.AddRangeAsync(CreateCourses());
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Course> CreateCourses()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "course-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var courseData = JsonSerializer.Deserialize<List<CourseJsonModel>>(jsonString, options);

            return courseData!.Select(CreateCourseFromJson);
        }

        private Course CreateCourseFromJson(CourseJsonModel jsonModel)
        {
            var details = new CourseDetails()
            {
                Subtitle = jsonModel.Details.Subtitle,
                Description = jsonModel.Details.Description,
                Level = Enum.Parse<CourseLevel>(jsonModel.Details.Level),
                ObjectivesSummary = new StringListValueObject(jsonModel.Details.ObjectivesSummary),
                MustKnowBefore = new StringListValueObject(jsonModel.Details.MustKnowBefore),
                IntendedFor = new StringListValueObject(jsonModel.Details.IntendedFor),
                ThumbnailUrl = new Uri(jsonModel.Details.ThumbnailUrl)
            };

            return CreateCourse(jsonModel.Title, jsonModel.CategoryName, jsonModel.SubcategoryName, details);
        }

        private Course CreateCourse(string title, string categoryName, string subcategoryName, CourseDetails details)
        {
            return new Course(title, _categories.First(x => x.Name == categoryName).Id, _subCategories.First(x => x.Name == subcategoryName).Id, _clock.CurrentDate(), details);
        }
    }
}
