using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Auth.Core.Entities
{
    internal class User
    {
        public Guid Id { get; set; }
        public Email Email { get; set; }
        public string Password { get; set; }
        public UserState State { get; set; }
        public DateTime CreatedAt { get; set; }

        //public Role Role { get; set; }
        //public string RoleId { get; set; }
    }
}
