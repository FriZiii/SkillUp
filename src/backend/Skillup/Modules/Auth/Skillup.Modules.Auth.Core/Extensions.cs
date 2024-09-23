using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Auth.Core.DAL;
using Skillup.Shared.Infrastructure.Postgres;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Auth.Api")]
namespace Skillup.Modules.Auth.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddPostgres<AuthDbContext>();
        }
    }
}
