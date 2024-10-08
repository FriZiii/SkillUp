using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.DTO
{
    public record TokensDto
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        public TokensDto(AuthTokens? tokens)
            : this(tokens?.AccessToken?.Token
                   ?? throw new TokensNullOrEmptyException(nameof(tokens.AccessToken)),
                   tokens?.RefreshToken?.Token
                   ?? throw new TokensNullOrEmptyException(nameof(tokens.RefreshToken)))
        { }

        public TokensDto(string? accessToken, string? refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new TokensNullOrEmptyException(nameof(accessToken));
            if (string.IsNullOrEmpty(refreshToken))
                throw new TokensNullOrEmptyException(nameof(refreshToken));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
