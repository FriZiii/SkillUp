namespace Skillup.Modules.Courses.Core.Options
{
    public class CourseDefaultValues
    {
        public string DefaultUserProfilePictureKey { get; set; } = Guid.NewGuid().ToString();
        public Uri DefaultTubnailPicture { get; set; }
    }
}
