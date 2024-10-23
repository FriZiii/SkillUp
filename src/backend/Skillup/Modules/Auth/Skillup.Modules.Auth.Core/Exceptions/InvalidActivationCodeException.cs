using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class InvalidActivationCodeException : SkillupException
    {
        public InvalidActivationCodeException() : base("Invalid activation code.")
        {
        }
    }
}
