using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.DAL;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Seeders.Data;
using Skillup.Shared.Abstractions.Seeder;
using System.Text.Json;

namespace Skillup.Modules.Finances.Core.Seeders
{
    internal class FinanceSeeder : ISeeder
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<Item> _items;
        private readonly DbSet<User> _users;
        private DbSet<Wallet> _wallets;
        private DbSet<Order> _orders;

        public FinanceSeeder(FinancesDbContext context)
        {
            _context = context;
            _items = context.Items;
            _users = context.Users;
            _wallets = context.Wallets;
            _orders = context.Orders;
        }
        public async Task Seed()
        {
            if (!await _items.AnyAsync())
            {
                await _items.AddRangeAsync(CreateItems());
                await _context.SaveChangesAsync();
            }

            if (!await _users.AnyAsync())
            {
                await _users.AddRangeAsync(CreateUsers());
                await _context.SaveChangesAsync();
            }

            if (!await _wallets.AnyAsync())
            {
                await _wallets.AddRangeAsync(CreateWallets());
                await _context.SaveChangesAsync();
            }

            if (!await _orders.AnyAsync())
            {
                var orderSeeder = new OrdersSeeder(_context);
                await orderSeeder.Seed();
            }
        }

        private IEnumerable<Item> CreateItems()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "items-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var courseData = JsonSerializer.Deserialize<List<ItemJsonModel>>(jsonString, options);

            return courseData!.Select(CreateItemFromJson);
        }

        private IEnumerable<User> CreateUsers()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "financesUsers-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var usersData = JsonSerializer.Deserialize<List<FinancesUserJsonModel>>(jsonString, options);

            return usersData!.Select(CreateUserFromJson);
        }

        private IEnumerable<Wallet> CreateWallets()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "financesUsers-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var usersData = JsonSerializer.Deserialize<List<FinancesUserJsonModel>>(jsonString, options);

            jsonString = File.ReadAllText(Path.Combine(path, "wallets-seeder-data.json"));
            var walletsData = JsonSerializer.Deserialize<List<WalletJsonModel>>(jsonString, options);

            var wallets = new List<Wallet>();

            foreach (var walletData in walletsData!)
            {
                var user = _users.Local.FirstOrDefault(u => u.Id == walletData.OwnerId)
                           ?? _users.FirstOrDefault(u => u.Id == walletData.OwnerId)
                           ?? CreateUserFromJson(usersData!.First(u => u.Id == walletData.OwnerId));

                wallets.Add(CreateWalletFromJson(walletData, user));
            }

            return wallets;
        }

        private Item CreateItemFromJson(ItemJsonModel jsonModel)
        {
            var item = new Item(jsonModel.Id, jsonModel.AuthorId, ItemType.Course, jsonModel.Price);

            return item;
        }

        private User CreateUserFromJson(FinancesUserJsonModel jsonModel)
        {
            var user = new User() { Id = jsonModel.Id };

            return user;
        }

        private Wallet CreateWalletFromJson(WalletJsonModel jsonModel, User owner)
        {
            var wallet = new Wallet(owner);
            wallet.AddToBalance(jsonModel.Balance);

            return wallet;
        }
    }
}
