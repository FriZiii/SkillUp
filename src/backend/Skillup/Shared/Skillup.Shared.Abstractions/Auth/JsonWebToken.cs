namespace Skillup.Shared.Abstractions.Auth
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public IDictionary<string, IEnumerable<string>> Claims { get; set; }


        public JsonWebToken(string token, long expiry, Guid userId, string role, IDictionary<string, IEnumerable<string>> claims)
        {
            Token = token;
            Expiry = expiry;
            UserId = userId;
            Role = role;
            Claims = claims;
        }

        public JsonWebToken(string token)
        {
            Token = token;
        }

        public static implicit operator string(JsonWebToken jsonWebToken) => jsonWebToken.Token;
        public static implicit operator JsonWebToken(string token) => new(token);
    }
}
