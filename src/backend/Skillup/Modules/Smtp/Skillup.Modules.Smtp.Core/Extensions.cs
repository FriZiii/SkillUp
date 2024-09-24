using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Mails.Core.Consumers;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Shared.Infrastructure.RabbitMQ;
using System.Reflection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Skillup.Modules.Mails.Api")]
namespace Skillup.Modules.Mails.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddScoped<ISmtpService, SmtpService>()
            .AddConsumer<SignedUpConsumer>();
    }
}