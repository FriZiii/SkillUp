using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Finances.Core.Consumers;
using Skillup.Modules.Finances.Core.DAL;
using Skillup.Modules.Finances.Core.DAL.Repositories;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.Seeders;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using Skillup.Shared.Infrastructure.Seeder;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Modules.Finances.Api")]
namespace Skillup.Modules.Finances.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddPostgres<FinancesDbContext>()
                .AddScoped<IItemRepository, ItemRepository>()
                .AddScoped<IWalletRepository, WalletRepository>()
                .AddScoped<IPurchaseHistoryRepository, PurchaseHistoryRepository>()
                .AddScoped<ICartRepository, CartRepository>()
                .AddScoped<IDiscountCodeRepository, DiscountCodeRepository>()
                .AddConsumer<ItemAddedConsumer>()
                .AddConsumer<SignedUpConsumer>()
                .AddSeeder<FinanceSeeder>();

        }
    }
}
