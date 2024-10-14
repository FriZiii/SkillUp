using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Modules.Courses.Infrastracture.Consumers;
using Skillup.Modules.Courses.Infrastracture.Repositories;
using Skillup.Modules.Courses.Infrastracture.Seeders;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using Skillup.Shared.Infrastructure.Seeder;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Courses.Api")]
namespace Skillup.Modules.Courses.Infrastracture
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var options = new CourseDefaultValues();
            configuration.GetSection("Courses:ModuleOptions:DefaultValues").Bind(CourseModuleOptions.DefaultValues);

            return services
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddSeeder<CourseModuleSeeder>()
                .AddPostgres<CoursesDbContext>()
                .AddConsumer<SignedUpConsumer>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ISubcategoryRepository, SubcategoryRepository>()
                .AddScoped<ISectionRepository, SectionRepository>()
                .AddScoped<IElementRepository, ElementRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISectionRepository, SectionRepository>();
        }
    }
}
