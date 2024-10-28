using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels;
using Skillup.Shared.Abstractions.S3;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class CourseUserSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly DbSet<User> _users;

        public CourseUserSeeder(CoursesDbContext context, IAmazonS3Service amazonS3Service)
        {
            _context = context;
            _amazonS3Service = amazonS3Service;
            _users = _context.Users;
        }

        public async Task Seed()
        {
            if (!await _users.AnyAsync())
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data", "Images", "default-profile-picture.png");
                IFormFile file = new FormFile(new FileStream(filePath, FileMode.Open, FileAccess.Read), 0, new FileInfo(filePath).Length, "file", Path.GetFileName(filePath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };

                await _amazonS3Service.Upload(file, S3FolderPaths.UserProfilePicture + CourseModuleOptions.DefaultValues.DefaultUserProfilePictureKey, true);

                await _users.AddRangeAsync(CreateUsers());
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> CreateUsers()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");
            var jsonString1 = File.ReadAllText(Path.Combine(path, "user-seeder-data.json"));

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonString2 = File.ReadAllText(Path.Combine(path, "courseUser-seeder-data.json"));


            var userList = JsonSerializer.Deserialize<List<UserJsonModel>>(jsonString1, options);
            var courseUserList = JsonSerializer.Deserialize<List<CourseUserJsonModel>>(jsonString2, options);

            courseUserList.Zip(userList, (courseUser, user) => new { courseUser, user })
                    .ToList()
                    .ForEach(x =>
              {
                  x.courseUser.Id = x.user.Id;
                  x.courseUser.Email = x.user.Email;
              });


            return courseUserList!.Select(CreateUserFromJson);

        }

        private User CreateUserFromJson(CourseUserJsonModel jsonModel)
        {
            var author = new User()
            {
                Id = jsonModel.Id,
                Email = jsonModel.Email,
                FirstName = jsonModel.FirstName,
                LastName = jsonModel.LastName,
                ProfilePictureKey = CourseModuleOptions.DefaultValues.DefaultUserProfilePictureKey,
                Details = new UserDetails()
                {
                    Title = jsonModel.Title,
                    Biography = jsonModel.Biography,
                }
            };

            return author;
        }
    }
}
