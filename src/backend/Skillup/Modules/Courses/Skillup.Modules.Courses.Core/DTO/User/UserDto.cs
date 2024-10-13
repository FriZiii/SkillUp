namespace Skillup.Modules.Courses.Core.DTO.User
{
    public abstract record UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Uri ProfilePicture { get; set; }
    }
}
