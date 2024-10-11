using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Core.DTO
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Email Email { get; set; }

        public string Title { get; set; }
        public string Biography { get; set; }

        public SocialMediaLinks SocialMediaLinks { get; set; }
    }
}
