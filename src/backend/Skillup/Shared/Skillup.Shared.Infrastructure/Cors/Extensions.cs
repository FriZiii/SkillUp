using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Skillup.Shared.Infrastructure.Cors
{
    internal static class Extensions
    {
        private static string _policyName = "SkillupPolicy";
        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            var options = services.GetOptions<CorsOptions>("Cors");

            services.AddCors(o =>
            {
                o.AddPolicy("cors",
                    builder => builder.WithOrigins(options.Origins)
                                      .AllowAnyMethod()
                                      .AllowCredentials()
                                      .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseCors(this IApplicationBuilder app)
        {
            app.UseCors(_policyName);

            return app;
        }
    }
}
