using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IWalletRepository
    {
        Task Add(Wallet wallet);
        Task<Wallet?> GetWallet(Guid walletId);
        Task<Wallet?> GetWalletByOwnerId(Guid ownerId);
        Task UpdateBalance(Wallet wallet);
    }
}
