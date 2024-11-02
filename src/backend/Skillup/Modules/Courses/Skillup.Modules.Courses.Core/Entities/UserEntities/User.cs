using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Core.Entities.UserEntities
{
    public class User
    {
        public Guid Id { get; set; }
        public required Email Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureKey { get; set; }
        public UserDetails Details { get; set; }
        public SocialMediaLinks SocialMediaLinks { get; set; }
        public PrivacySettings PrivacySettings { get; set; }

        public IEnumerable<UserPurchasedCourse> PurchasedCourses { get; set; }
        public IEnumerable<Course> CreatedCoures { get; set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            ProfilePictureKey = CourseModuleOptions.DefaultValues.DefaultUserProfilePictureKey;
            Details = new();
            SocialMediaLinks = new();
            PrivacySettings = new PrivacySettings();

            PurchasedCourses = Enumerable.Empty<UserPurchasedCourse>();
        }
    }
}
