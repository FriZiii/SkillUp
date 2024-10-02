using Microsoft.Extensions.DependencyInjection;

namespace Skillup.Shared.Infrastructure.Client
{
    internal static class Extensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services)
        {
            var clientOptions = services.GetOptions<ClientOptions>("Client");
            return services.AddSingleton(clientOptions);
        }
    }
}
