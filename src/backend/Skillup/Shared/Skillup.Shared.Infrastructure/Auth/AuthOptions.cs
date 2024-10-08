namespace Skillup.Shared.Infrastructure.Auth
{
    public class AuthOptions
    {
        public string IssuerSigningKey { get; set; }
        public string Issuer { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }

        public string ValidAudience { get; set; }
        public bool ValidateAudience { get; set; }

        public bool ValidateLifetime { get; set; }
        public bool RequireExpirationTime { get; set; }
        public int TokenExpireSeconds { get; set; }
        public int RefreshTokenExpireSeconds { get; set; }

        public bool RequireHttpsMetadata { get; set; }
    }
}
