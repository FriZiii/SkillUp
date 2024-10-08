namespace Skillup.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        public AuthTokens CreateTokens(Guid userId, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null);
        public AuthTokens RefreshAccessToken(string refreshToken, Guid userId, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null);
        public Guid? GetUserIdFromExpiredToken(string accessToken);
    }
}
