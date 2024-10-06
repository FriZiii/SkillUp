using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Auth.Core.DAL;
using Skillup.Modules.Auth.Core.DAL.Repositories;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Seeders;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Infrastructure;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.Seeder;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Auth.Api")]
namespace Skillup.Modules.Auth.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            var registrationOptions = services.GetOptions<RegistrationOptions>("auth:registration");
            services.AddSingleton(registrationOptions);

            return services
                .AddPostgres<AuthDbContext>()
                .AddSingleton<IAuthTokenStorage, AuthTokenStorage>()
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddSeeder<AccountSeeder>()
                .AddScoped<IUserRepository, UserRepository>();
        }
    }
}
