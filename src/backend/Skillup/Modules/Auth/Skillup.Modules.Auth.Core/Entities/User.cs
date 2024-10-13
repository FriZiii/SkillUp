using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Auth.Core.Entities
{
    internal class User
    {
        public User(Guid id, Email email, UserRole role, UserState state, DateTime createdAt, Guid activationToken, DateTime tokenExpiration)
        {
            Id = id;
            Email = email;
            Role = role;
            State = state;
            CreatedAt = createdAt;
            ActivationToken = activationToken;
            TokenExpiration = tokenExpiration;
        }

        public Guid Id { get; set; }
        public Email Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public UserState State { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ActivationToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
