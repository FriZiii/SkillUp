using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Skillup.Shared.Abstractions.Auth;
using System.Text;

namespace Skillup.Shared.Infrastructure.Auth
{
    internal static class Extensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            var options = services.GetOptions<AuthOptions>("Auth");
            services.AddSingleton<IAuthManager, AuthManager>();

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)) ?? throw new ArgumentException("Missing issuer signing key.", nameof(options.IssuerSigningKey))
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = options.RequireHttpsMetadata;
                o.TokenValidationParameters = tokenValidationParameters;
                o.MapInboundClaims = false;
            });

            services.AddSingleton(options);

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
