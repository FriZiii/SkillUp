namespace Skillup.Modules.Courses.Core.Entities.UserEntities
{
    public class UserDetails
    {
        public UserDetails(string title, string biography)
        {
            Title = title;
            Biography = biography;
        }
        public UserDetails()
        {

        }

        public string Title { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
    }
}
