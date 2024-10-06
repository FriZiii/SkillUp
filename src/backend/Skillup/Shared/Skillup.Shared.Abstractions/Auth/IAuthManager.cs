namespace Skillup.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        public AuthTokens CreateTokens(Guid userId, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null);
    }
}
