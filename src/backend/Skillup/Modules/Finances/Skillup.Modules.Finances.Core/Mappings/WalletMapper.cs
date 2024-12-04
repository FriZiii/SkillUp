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
    }
}
