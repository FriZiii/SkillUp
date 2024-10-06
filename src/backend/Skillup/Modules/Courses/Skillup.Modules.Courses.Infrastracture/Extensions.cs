using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Infrastracture.Repositories;
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
                .AddPostgres<CoursesDbContext>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ISubcategoryRepository, SubcategoryRepository>();
        }
    }
}
