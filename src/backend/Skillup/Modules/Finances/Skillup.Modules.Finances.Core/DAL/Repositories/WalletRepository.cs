using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

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

        public async Task CreateUserWallet(Wallet userWallet)
        {
            await _wallets.AddAsync(userWallet);
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet?> GetWalletByUserId(Guid userId)
            => await _wallets.FirstOrDefaultAsync(x => x.Id.Equals(userId));

        public async Task AddBalanceToWalletByUserId(Guid userId, decimal amount)
        {
            var wallet = await _wallets.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException(userId);
            wallet.AddToBalance(amount);
            await _context.SaveChangesAsync();
        }

        public async Task SubtractBalanceFromWalletByUserId(Guid userId, decimal amount)
        {
            var wallet = await _wallets.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException(userId);
            wallet.SubtractFromBalance(amount);
            await _context.SaveChangesAsync();
        }
    }
}
