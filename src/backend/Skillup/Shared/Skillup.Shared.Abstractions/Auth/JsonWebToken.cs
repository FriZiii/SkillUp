namespace Skillup.Shared.Abstractions.Auth
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public IDictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}
