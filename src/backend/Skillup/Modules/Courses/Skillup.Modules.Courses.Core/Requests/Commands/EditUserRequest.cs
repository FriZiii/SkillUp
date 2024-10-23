using MediatR;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands
{
    public record EditUserRequest : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string Title { get; set; }
        public SocialMediaLinks SocialMediaLinks { get; set; }
    };
}
