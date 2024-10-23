using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Exceptions;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class ItemRepository : IItemRepository
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<Item> _items;

        public ItemRepository(FinancesDbContext context)
        {
            _context = context;
            _items = context.Items;
        }

        public async Task Add(Item item)
        {
            await _items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Guid itemId, Currency currency)
        {
            var itemToEdit = await _items.FirstOrDefaultAsync(x => x.Id == itemId) ?? throw new ItemNotFoundException(itemId);
            itemToEdit.Price = currency;
            await _context.SaveChangesAsync();
        }

        public async Task<Item> GetById(Guid itemId)
        {
            var item = await _items.FirstOrDefaultAsync(x => x.Id == itemId) ?? throw new ItemNotFoundException(itemId);
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsByType(ItemType type)
        {
            return await _items.Where(x => x.Type == type).ToListAsync();
        }
    }
}
