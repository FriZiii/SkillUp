using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Services
{
    internal interface IDiscountCodeService
    {
        public Task<Currency> ApplyDiscount(Guid discountCodeId, Item item);
    }
}
