using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using Skillup.Shared.Abstractions.S3;
using Skillup.Shared.Abstractions.Time;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseSeeder
    {
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly IClock _clock;

        private List<Category> _categories = new();
        private List<Subcategory> _subCategories = new();

        public CourseSeeder(CoursesDbContext context, IClock clock, IAmazonS3Service amazonS3Service)
        {
            _amazonS3Service = amazonS3Service;
            _context = context;
            _courses = context.Courses;
            _clock = clock;
        }

        public async Task Seed()
        {
            _categories = await _context.Categories.ToListAsync();
            _subCategories = await _context.Subcategories.ToListAsync();

            var filePath = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data", "Images", "default-tumbnail-picture.png");
            IFormFile file = new FormFile(new FileStream(filePath, FileMode.Open, FileAccess.Read), 0, new FileInfo(filePath).Length, "file", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            await _amazonS3Service.Upload(file, S3FolderPaths.CourseTubnailPicture + CourseModuleOptions.DefaultValues.DefaultTubnailPictureKey);

            if (!await _courses.AnyAsync())
            {
                await _courses.AddRangeAsync(CreateCourses());
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Course> CreateCourses()
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
                ThumbnailKey = CourseModuleOptions.DefaultValues.DefaultTubnailPictureKey
            };

            return CreateCourse(jsonModel.Id, jsonModel.AuthorId, jsonModel.Title, jsonModel.CategoryName, jsonModel.SubcategoryName, details);
        }

        private Course CreateCourse(Guid id, Guid authorId, string title, string categoryName, string subcategoryName, CourseDetails details)
        {
            return new Course(id, authorId, title, _categories.First(x => x.Name == categoryName).Id, _subCategories.First(x => x.Name == subcategoryName).Id, _clock.CurrentDate(), details);
        }
    }
}
