namespace Skillup.Modules.Courses.Core.Options
{
    public class CourseDefaultValues
    {
        public string DefaultUserProfilePictureKey { get; set; } = Guid.NewGuid().ToString();
        public string DefaultTubnailPictureKey { get; set; } = Guid.NewGuid().ToString();
    }
}
