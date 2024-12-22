using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Mails.Core.Consumers;
using Skillup.Modules.Mails.Core.DAL;
using Skillup.Modules.Mails.Core.DAL.Repositories;
using Skillup.Modules.Mails.Core.Repositories;
using Skillup.Modules.Mails.Core.Seeders;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using Skillup.Shared.Infrastructure.Seeder;
using System.Reflection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Skillup.Modules.Mails.Api")]
namespace Skillup.Modules.Mails.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddPostgres<MailDbContext>()
            .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddConsumer<SignedUpConsumer>()
            .AddConsumer<PasswordChangedConsumer>()
            .AddConsumer<PasswordResetRequestedConsumer>()
            .AddScoped<ISmtpService, SmtpService>()
            .AddSeeder<MailSeeder>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}