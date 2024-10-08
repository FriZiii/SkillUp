using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            var connectionStringTemplate = services.GetSection("Postgres:ConnectionString").Value;
            if (string.IsNullOrEmpty(connectionStringTemplate))
                throw new InvalidOperationException("Connection string template cannot be null or empty.");

            PostgresOptions options = (PostgresOptions)new PostgresOptions(connectionStringTemplate).InjectEnvironment();

            services.AddSingleton(options);

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<PostgresOptions>();

            services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));
            return services;
        }
    }
}
