using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class WalletMapper
    {
        public WalletDto WalletToDto(Wallet wallet)
        {
            return new WalletDto()
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UserId = wallet.OwnerId,
            };
        }

        public WalletWithBalanceHistoryDto WalletToWalletWithBalanceHistoryDto(Wallet wallet, IEnumerable<BalanceHistory> balanceHistories)
        {
            var balanceHistoryMapper = new BalanceHistoryMapper();
            return new WalletWithBalanceHistoryDto()
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UserId = wallet.OwnerId,
                BalanceHistory = balanceHistories.Select(balanceHistoryMapper.BalanceHistoryToDto)
            };
        }
    }
}
