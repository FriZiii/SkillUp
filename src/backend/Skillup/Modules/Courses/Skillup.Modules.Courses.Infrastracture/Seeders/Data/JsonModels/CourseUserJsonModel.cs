namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class CourseUserJsonModel
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri ProfilePicture { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
    }

    internal class UserJsonModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
