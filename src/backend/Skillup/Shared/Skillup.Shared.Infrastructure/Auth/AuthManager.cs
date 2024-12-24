using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Skillup.Shared.Infrastructure.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtOptions _options;
        private readonly IClock _clock;
        private readonly ILogger<AuthManager> _logger;
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;

        public AuthManager(JwtOptions options, IClock clock, ILogger<AuthManager> logger)
        {
            _options = options;
            _clock = clock;
            _logger = logger;
            _signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256
            );
            _issuer = options.Issuer;
        }

        public AuthTokens CreateTokens(Guid userId, UserRole userRole, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            var now = _clock.CurrentDate();
            return new AuthTokens
            {
                AccessToken = JwtTokenUtils.CreateToken(_options, userId, now, _options.TokenExpireSeconds, userRole, claims),
                RefreshToken = JwtTokenUtils.CreateToken(_options, userId, now, _options.RefreshTokenExpireSeconds)
            };
        }

        public AuthTokens RefreshAccessToken(string refreshToken, Guid userId, UserRole userRole, IDictionary<string, IEnumerable<string>>? claims = null)
        {
            try
            {
                var principal = JwtTokenUtils.ValidateRefreshToken(_options, refreshToken);
                if (principal == null || principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value != userId.ToString())
                {
                    _logger.LogError("Invalid token, Sub not found");
                    throw new SecurityTokenException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SecurityTokenException();
            }

            var now = _clock.CurrentDate();
            return new AuthTokens
            {
                AccessToken = JwtTokenUtils.CreateToken(_options, userId, now, _options.TokenExpireSeconds, userRole, claims),
                RefreshToken = refreshToken,
            };
        }

        public Guid? GetUserIdFromExpiredToken(string accessToken)
        {
            var userIdClaim = JwtTokenUtils.GetClaimFromToken(accessToken, JwtRegisteredClaimNames.Sub);
            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
        }
    }
}
