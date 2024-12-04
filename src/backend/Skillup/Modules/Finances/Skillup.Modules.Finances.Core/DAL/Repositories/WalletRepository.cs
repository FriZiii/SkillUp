using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{

    internal class WalletRepository : IWalletRepository
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<Wallet> _wallets;

        public WalletRepository(FinancesDbContext context)
        {
            _context = context;
            _wallets = _context.Wallets;
        }

        public async Task Add(Wallet wallet)
        {
            await _wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBalance(Wallet wallet)
        {
            var walletToUpdate = await _wallets.FirstOrDefaultAsync(x => x.Id == wallet.Id);

            walletToUpdate = new Wallet(wallet);

            await _context.SaveChangesAsync();
        }

        public async Task<Wallet?> GetWallet(Guid walletId)
            => await _wallets.FirstOrDefaultAsync(x => x.Id == walletId);

        public async Task<Wallet?> GetWalletByOwnerId(Guid ownerId)
            => await _wallets.FirstOrDefaultAsync(x => x.OwnerId == ownerId);
    }
}
