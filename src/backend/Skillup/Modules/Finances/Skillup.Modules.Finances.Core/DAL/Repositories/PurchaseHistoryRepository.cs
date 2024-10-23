using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<PurchaseHistory> _purchaseHistories;

        public PurchaseHistoryRepository(FinancesDbContext context)
        {
            _context = context;
            _purchaseHistories = _context.PurchaseHistories;
        }

        public async Task Add(PurchaseHistory purchaseHistory)
        {
            await _purchaseHistories.AddAsync(purchaseHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> GetByAuthorId(Guid authorId)
            => await _purchaseHistories
                .Include(ph => ph.Item)
                .Where(ph => ph.Item.AuthorId == authorId)
                .GroupBy(ph => ph.Item)
                .ToListAsync();

        public async Task<IEnumerable<PurchaseHistory>> GetByItemId(Guid itemId)
            => await _purchaseHistories.Where(x => x.ItemId == itemId).ToListAsync();

        public async Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> GetByUserId(Guid userId)
            => await _purchaseHistories
                .Include(ph => ph.Item)
                .Where(ph => ph.UserWalletId == userId)
                .GroupBy(ph => ph.Item)
                .ToListAsync();
    }
}
