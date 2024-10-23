using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IItemRepository
    {
        Task Add(Item item);
        Task Edit(Guid itemId, Currency currency);
        Task<Item> GetById(Guid itemId);
        Task<IEnumerable<Item>> GetItemsByType(ItemType type);
    }
}
