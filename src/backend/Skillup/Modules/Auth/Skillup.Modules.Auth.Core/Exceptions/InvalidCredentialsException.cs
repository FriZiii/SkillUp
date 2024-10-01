using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class InvalidCredentialsException : SkillupException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
