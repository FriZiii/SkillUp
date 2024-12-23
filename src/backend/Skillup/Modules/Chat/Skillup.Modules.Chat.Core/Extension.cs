using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Chat.Core.Consumers;
using Skillup.Modules.Chat.Core.DAL;
using Skillup.Modules.Chat.Core.DAL.Repositories;
using Skillup.Modules.Chat.Core.Repositories;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using System.Reflection;
using System.Runtime.CompilerServices;



[assembly: InternalsVisibleTo("Skillup.Modules.Chat.Api")]
namespace Skillup.Modules.Chat.Core
{
    internal static class Extension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services.AddPostgres<ChatDbContext>()
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddConsumer<CoursePurchasedConsumer>()
                .AddScoped<IChatRepository, ChatRepository>();
        }
    }
}
