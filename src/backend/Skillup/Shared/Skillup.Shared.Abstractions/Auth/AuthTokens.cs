namespace Skillup.Shared.Abstractions.Auth
{
    public class AuthTokens
    {
        public JsonWebToken AccessToken { get; set; }
        public JsonWebToken RefreshToken { get; set; }
    }
}
