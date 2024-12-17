using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IWalletRepository
    {
        Task Add(Wallet wallet);
        Task<Wallet?> GetWallet(Guid walletId);
        Task<Wallet?> GetWalletByOwnerId(Guid ownerId);
        Task<Guid> UpdateBalance(Wallet wallet);
        Task<IEnumerable<BalanceHistory>> GetBalanceHistoryByWalletId(Guid walletId);
    }
}
