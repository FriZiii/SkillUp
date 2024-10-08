using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class ActivationCodeExpiredException : SkillupException
    {
        public ActivationCodeExpiredException() : base("Activation code has expired.")
        {
        }
    }
}
