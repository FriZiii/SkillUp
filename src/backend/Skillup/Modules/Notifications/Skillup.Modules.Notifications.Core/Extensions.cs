using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Notifications.Core.Consumers;
using Skillup.Modules.Notifications.Core.DAL;
using Skillup.Modules.Notifications.Core.DAL.Repositories;
using Skillup.Modules.Notifications.Core.Repositories;
using Skillup.Modules.Notifications.Core.Seeders;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using Skillup.Shared.Infrastructure.Seeder;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Notifications.Api")]
namespace Skillup.Modules.Notifications.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddPostgres<NotificationsDbContext>()
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddConsumer<NotificationPublishedConsumer>()
                .AddConsumer<SignedUpConsumer>()
                .AddSeeder<NotificationsSeeder>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<INotificationRepository, NotificationRepository>();
        }
    }
}
