using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class EmailInUseException : SkillupException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}
