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

        public FinanceSeeder(FinancesDbContext context)
        {
            _context = context;
            _items = context.Items;
        }
        public async Task Seed()
        {
            if (!await _items.AnyAsync())
            {
                await _items.AddRangeAsync(CreateItems());
                await _context.SaveChangesAsync();
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

        private Item CreateItemFromJson(ItemJsonModel jsonModel)
        {
            var item = new Item(jsonModel.Id, jsonModel.AuthorId, ItemType.Course, jsonModel.Price);

            return item;
        }
    }
}
