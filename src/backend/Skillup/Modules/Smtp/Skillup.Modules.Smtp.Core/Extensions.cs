using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Infrastructure.EnvironmentInjector;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Skillup.Modules.Mails.Api")]
namespace Skillup.Modules.Mails.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        SmtpOptions options = (SmtpOptions)new SmtpOptions().InjectEnvironment();
        services.AddSingleton(options);
        return services;
    }
}