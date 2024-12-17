using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IWalletRepository
    {
        Task Add(Wallet wallet);
        Task<Wallet?> GetWallet(Guid walletId);
        Task<Wallet?> GetWalletByOwnerId(Guid ownerId);
        Task UpdateWalletBalance(Wallet wallet, BalanceHistory balanceHistory);
        Task<IEnumerable<BalanceHistory>> GetBalanceHistoryByWalletId(Guid walletId);
    }
}
