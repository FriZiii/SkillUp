using Microsoft.IdentityModel.Tokens;
using Skillup.Shared.Abstractions.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skillup.Shared.Infrastructure.Auth
{
    public static class JwtTokenUtils
    {
        private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = [];
        private static readonly JwtSecurityTokenHandler _tokenHandler = new() { MapInboundClaims = false };

        public static TokenValidationParameters GetTokenValidationParameters(JwtOptions options)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = options.ValidateIssuer,
                ValidIssuer = options.ValidIssuer,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ValidateAudience = options.ValidateAudience,
                ValidAudience = options.ValidAudience,
                ValidateLifetime = options.ValidateLifetime,
                RequireExpirationTime = options.RequireExpirationTime,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey))
            };
        }

        public static ClaimsPrincipal ValidateRefreshToken(JwtOptions options, string refreshToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = options.ValidateIssuer,
                ValidIssuer = options.ValidIssuer,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ValidateAudience = options.ValidateAudience,
                ValidAudience = options.ValidAudience,
                ValidateLifetime = options.ValidateLifetime,
                RequireExpirationTime = options.RequireExpirationTime,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey))
            };

            return _tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out _);
        }

        public static JsonWebToken CreateToken(JwtOptions options, Guid userId, DateTime now, int expireSeconds, UserRole? role = null, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            var expires = now.AddSeconds(expireSeconds);
            var jwtClaims = BuildClaims(options, userId, now, role, claims);

            SigningCredentials _signingCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.ValidAudience,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken(token, new DateTimeOffset(expires).ToUnixTimeMilliseconds(), userId, role, claims ?? EmptyClaims);
        }

        public static Claim? GetClaimFromToken(string token, string jwtClaimName)
        {
            var jwtToken = _tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            return userIdClaim;
        }

        private static List<Claim> BuildClaims(JwtOptions options, Guid userId, DateTime now, UserRole? userRole = null, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString()),
                new(JwtRegisteredClaimNames.Aud, options.ValidAudience)
            };

            if (userRole != null)
            {
                jwtClaims.Add(new(ClaimTypes.Role, userRole.Value.ToString()));
            }

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
