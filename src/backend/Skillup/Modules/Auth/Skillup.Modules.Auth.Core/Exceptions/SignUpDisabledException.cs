using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class SignUpDisabledException : SkillupException
    {
        public SignUpDisabledException() : base("Sign up is disabled.")
        {
        }
    }
}
