using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class TokenValidationFailed : SkillupException
    {
        public TokenValidationFailed() : base("Token validation failed.")
        {
        }
    }
}
