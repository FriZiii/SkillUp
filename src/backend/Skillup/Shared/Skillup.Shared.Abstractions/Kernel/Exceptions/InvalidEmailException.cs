using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Shared.Abstractions.Kernel.Exceptions
{
    public class InvalidEmailException : SkillupException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
        {
            Email = email;
        }
    }
}
