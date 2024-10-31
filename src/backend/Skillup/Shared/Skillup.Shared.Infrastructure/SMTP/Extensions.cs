using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Abstractions;
using Skillup.Shared.Infrastructure.EnvironmentInjector;
using Skillup.Shared.Infrastructure.S3;

namespace Skillup.Shared.Infrastructure.SMTP
{
    public static class Extensions
    {
        public static IServiceCollection AddSmpt(this IServiceCollection services)
        {
            var options = (AmazonS3Options)services.GetOptions<SmtpOptions>("Smtp").InjectEnvironment();
            services.AddSingleton(options);

            return services;
        }
    }
}
