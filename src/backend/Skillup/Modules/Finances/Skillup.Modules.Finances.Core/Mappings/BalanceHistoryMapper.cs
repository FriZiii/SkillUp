using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class BalanceHistoryMapper
    {
        public BalanceHistoryDto BalanceHistoryToDto(BalanceHistory balanceHistory)
        {
            return new BalanceHistoryDto(balanceHistory.Amount, balanceHistory.Date, balanceHistory.Type);
        }
    }
}
