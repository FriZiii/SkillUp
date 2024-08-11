using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Auth.Infrastructure.Ef;
using Skillup.Shared.Infrastructure.Postgres;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Auth.Api")]
namespace Skillup.Modules.Auth.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddPostgres<AuthDbContext>();
        }
    }
}
