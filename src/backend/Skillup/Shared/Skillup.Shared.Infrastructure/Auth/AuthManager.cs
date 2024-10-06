using Microsoft.IdentityModel.Tokens;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Time;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skillup.Shared.Infrastructure.Auth
{
    public class AuthManager : IAuthManager
    {
        private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = [];
        private readonly AuthOptions _options;
        private readonly IClock _clock;
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;

        public AuthManager(AuthOptions options, IClock clock)
        {
            _options = options;
            _clock = clock;
            _signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256
            );
            _issuer = options.Issuer;
        }

        public AuthTokens CreateTokens(Guid userId, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            var now = _clock.CurrentDate();
            return new AuthTokens
            {
                AccessToken = CreateToken(userId, now, _options.TokenExpireSeconds, role, claims),
                RefreshToken = CreateToken(userId, now, _options.RefreshTokenExpireSeconds)
            };
        }

        private JsonWebToken CreateToken(Guid userId, DateTime now, int expireSeconds, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            var expires = now.AddSeconds(expireSeconds);
            var jwtClaims = BuildClaims(userId, now, role, claims);

            var jwt = new JwtSecurityToken(
                _issuer,
                audience: _options.ValidAudience,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
                UserId = userId,
                Role = role ?? string.Empty,
                Claims = claims ?? EmptyClaims
            };
        }

        private List<Claim> BuildClaims(Guid userId, DateTime now, string? role, IDictionary<string, IEnumerable<string>>? claims)
        {
            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, _options.ValidAudience));

            if (claims?.Any() == true)
            {
                foreach (var (claimType, values) in claims)
                {
                    jwtClaims.AddRange(values.Select(value => new Claim(claimType, value)));
                }
            }

            return jwtClaims;
        }
    }
}
