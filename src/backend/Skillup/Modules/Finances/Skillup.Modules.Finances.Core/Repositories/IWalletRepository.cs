using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IWalletRepository
    {
        Task CreateUserWallet(Wallet userWallet);
        Task<Wallet?> GetWalletByUserId(Guid userId);
        Task AddBalanceToWalletByUserId(Guid userId, decimal amount);
        Task SubtractBalanceFromWalletByUserId(Guid userId, decimal amount);
    }
}
