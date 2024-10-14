using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IPurchaseHistoryRepository
    {
        Task Add(PurchaseHistory purchaseHistory);
        Task<IEnumerable<PurchaseHistory>> GetByItemId(Guid itemId);
        Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> GetByAuthorId(Guid authorId);
        Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> GetByUserId(Guid userId);
    }
}
