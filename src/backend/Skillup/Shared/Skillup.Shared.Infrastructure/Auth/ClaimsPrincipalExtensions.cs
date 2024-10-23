using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Skillup.Shared.Infrastructure.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub);
            return claim != null ? Guid.Parse(claim.Value) : null;
        }
    }
}
