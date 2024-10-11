using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Core.Entities.UserEntities
{
    public class User
    {
        public Guid Id { get; set; }
        public Uri? ProfilePicture { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required Email Email { get; set; }
        public UserDetails? Details { get; set; }
        public SocialMediaLinks? SocialMediaLinks { get; set; }
        public PrivacySettings PrivacySettings { get; set; } = new PrivacySettings();

        public IEnumerable<UserPurchasedCourse>? PurchasedCourses { get; set; }
    }
}
