using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skillup.Shared.Abstractions.Seeder;

namespace Skillup.Shared.Infrastructure.Seeder
{
    internal class DatabaseSeederService(IServiceProvider serviceProvider) : IHostedService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();
            var tasks = seeders.Select(seeder => seeder.Seed());
            await Task.WhenAll(tasks);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
