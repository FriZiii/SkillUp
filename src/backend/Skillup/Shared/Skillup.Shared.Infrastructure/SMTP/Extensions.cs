using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Abstractions;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.SMTP
{
    public static class Extensions
    {
        public static IServiceCollection AddSmpt(this IServiceCollection services)
        {
            var options = (SmtpOptions)services.GetOptions<SmtpOptions>("Smtp").InjectEnvironment();
            services.AddSingleton(options);

            return services;
        }
    }
}
