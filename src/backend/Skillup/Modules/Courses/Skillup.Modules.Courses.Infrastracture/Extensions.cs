using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Infrastracture.Repositories;
using Skillup.Shared.Infrastructure.Postgres;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Courses.Api")]
namespace Skillup.Modules.Courses.Infrastracture
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddPostgres<CoursesDbContext>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ISubcategoryRepository, SubcategoryRepository>()
                .AddScoped<ISectionRepository, SectionRepository>();
        }
    }
}
