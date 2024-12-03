using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.ValueObjects
{
    internal class PercentageDiscountCode : DiscountCode
    {
        public PercentageDiscountCode(AddDiscountCodeDto dto)
            : base(dto)
        {
            if (dto.DiscountValue <= 0 || dto.DiscountValue > 100)
                throw new Exception(); //TODO: Custom ex;
        }

        private PercentageDiscountCode() { } // Only for Ef core
    }

    internal class FixedAmountDiscountCode : DiscountCode
    {
        public FixedAmountDiscountCode(AddDiscountCodeDto dto)
            : base(dto)
        {
        }

        private FixedAmountDiscountCode() { } // Only for Ef core
    }
}
