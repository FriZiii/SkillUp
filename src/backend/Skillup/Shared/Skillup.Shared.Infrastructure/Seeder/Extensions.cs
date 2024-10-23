using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Abstractions.Seeder;

namespace Skillup.Shared.Infrastructure.Seeder
{
    public static class Extensions
    {
        public static IServiceCollection AddSeeder<T>(this IServiceCollection services) where T : class, ISeeder
        {
            services.AddScoped<ISeeder, T>();
            return services;
        }
    }
}
