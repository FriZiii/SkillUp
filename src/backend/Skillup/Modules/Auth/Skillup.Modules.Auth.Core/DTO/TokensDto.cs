using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Auth.Core.DTO
{
    public record TokensDto
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        public TokensDto(AuthTokens? tokens)
            : this(tokens?.AccessToken?.Token
                   ?? throw new BadRequestException("Invalid auth token"),
                   tokens?.RefreshToken?.Token
                   ?? throw new BadRequestException("Invalid auth token"))
        { }

        public TokensDto(string? accessToken, string? refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new BadRequestException("Invalid auth token");
            if (string.IsNullOrEmpty(refreshToken))
                throw new BadRequestException("Invalid auth token");

            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
