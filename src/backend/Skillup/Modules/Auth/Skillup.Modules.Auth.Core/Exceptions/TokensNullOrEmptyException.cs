using Skillup.Shared.Abstractions.Exceptions;

namespace Skillup.Modules.Auth.Core.Exceptions
{
    internal class TokensNullOrEmptyException : SkillupException
    {
        public string TokenName { get; }
        public TokensNullOrEmptyException(string tokenName) : base($"Token '{tokenName}' cannot be null or empty")
        {
            TokenName = tokenName;
        }
    }
}
