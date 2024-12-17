using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{

    internal class WalletRepository : IWalletRepository
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<Wallet> _wallets;
        private readonly DbSet<BalanceHistory> _balanceHistories;

        public WalletRepository(FinancesDbContext context)
        {
            _context = context;
            _wallets = _context.Wallets;
            _balanceHistories = _context.BalanceHistories;
        }

        public async Task Add(Wallet wallet)
        {
            await _wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> UpdateBalance(Wallet wallet)
        {
            var walletToUpdate = await _wallets.FirstOrDefaultAsync(x => x.Id == wallet.Id) ?? throw new Exception(); //TODO: Custom ex: Wallet doesnt exist

            var history = new BalanceHistory(wallet.Id, wallet.Balance);

            if (wallet.Balance < walletToUpdate.Balance) //Subtract
            {
                history.Type = "Subtract";
            }
            else // Add
            {
                history.Type = "Add";
            }
            _balanceHistories.Add(history);

            walletToUpdate = new Wallet(wallet);

            await _context.SaveChangesAsync();

            return history.Id;
        }

        public async Task<Wallet?> GetWallet(Guid walletId)
            => await _wallets.FirstOrDefaultAsync(x => x.Id == walletId);

        public async Task<Wallet?> GetWalletByOwnerId(Guid ownerId)
            => await _wallets.FirstOrDefaultAsync(x => x.OwnerId == ownerId);

        public async Task<IEnumerable<BalanceHistory>> GetBalanceHistoryByWalletId(Guid walletId)
            => await _balanceHistories.Where(x => x.WalletId == walletId).ToListAsync();
    }
}
