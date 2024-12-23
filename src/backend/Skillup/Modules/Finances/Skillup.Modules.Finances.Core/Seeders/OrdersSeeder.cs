using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.DAL;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Shared.Abstractions.Seeder;

namespace Skillup.Modules.Finances.Core.Seeders
{
    internal class OrdersSeeder(FinancesDbContext context) : ISeeder
    {
        private readonly FinancesDbContext _context = context;

        public async Task Seed()
        {
            await SeedOrders(10_000);
        }

        public async Task SeedOrders(int numberOfOrders)
        {
            var users = await _context.Users.ToListAsync();
            var items = await _context.Items.ToListAsync();
            var wallets = await _context.Wallets.ToListAsync();

            var orders = new List<Order>();

            for (int i = 0; i < numberOfOrders; i++)
            {
                var orderItems = GenerateOrderItems(items);

                var randomUser = users[new Random().Next(users.Count)];
                var balanceHistory = new BalanceHistory(wallets.First(x => x.OwnerId == randomUser.Id).Id, 100, "", "");

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.UtcNow.AddDays(-new Random().Next(1, 365)),
                    TotalPrice = orderItems.Sum(x => x.ItemPrice),
                    OrdererId = randomUser.Id,
                    BalanceHistory = balanceHistory,
                    Items = orderItems
                };

                orders.Add(order);
            }

            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();
        }

        private ICollection<OrderItem> GenerateOrderItems(List<Item> items)
        {
            var orderItems = new List<OrderItem>();
            int itemCount = new Random().Next(1, 6);

            for (int i = 0; i < itemCount; i++)
            {
                var item = items[new Random().Next(items.Count)];

                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ItemId = item.Id,
                    ItemPrice = item.Price,
                    OrderId = Guid.NewGuid()
                });
            }

            return orderItems;
        }
    }
}
