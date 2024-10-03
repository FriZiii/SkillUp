using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Infrastructure.Postgres;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Courses.Api")]
namespace Skillup.Modules.Courses.Infrastracture
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPostgres<CoursesDbContext>();
        }
    }
}
