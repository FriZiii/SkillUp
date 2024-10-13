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
        public Uri ProfilePicture { get; set; }
        public UserDetails Details { get; set; }
        public SocialMediaLinks SocialMediaLinks { get; set; }
        public PrivacySettings PrivacySettings { get; set; }

        public IEnumerable<UserPurchasedCourse> PurchasedCourses { get; set; }
        public IEnumerable<Course> CreatedCoures { get; set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            ProfilePicture = CourseModuleOptions.DefaultValues.DefaultUserProfilePicture;
            Details = new();
            SocialMediaLinks = new();
            PrivacySettings = new PrivacySettings();

            PurchasedCourses = Enumerable.Empty<UserPurchasedCourse>();
        }


        public User(Guid id, string email, string firstName, string lastName, Uri profilePicture, UserDetails details)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            ProfilePicture = profilePicture;
            Details = details;
            SocialMediaLinks = new();
            PrivacySettings = new PrivacySettings();

            PurchasedCourses = Enumerable.Empty<UserPurchasedCourse>();
        }
    }
}
