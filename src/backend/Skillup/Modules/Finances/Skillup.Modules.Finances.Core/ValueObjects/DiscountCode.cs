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

        public override Currency Apply(Item item)
        {
            if (!CanBeUsed())
            {
                throw new InvalidOperationException("Discount code has reached its maximum usage."); //TODO: Custom EX
            }

            var discountedPrice = item.Price * (1 - DiscountValue / 100);
            IncrementUsesCount();
            return new Currency(discountedPrice);
        }
    }

    internal class FixedAmountDiscountCode : DiscountCode
    {
        public FixedAmountDiscountCode(AddDiscountCodeDto dto)
            : base(dto)
        {
        }

        private FixedAmountDiscountCode() { } // Only for Ef core

        public override Currency Apply(Item item)
        {
            if (!CanBeUsed())
            {
                throw new InvalidOperationException("Discount code has reached its maximum usage."); //TODO: Custom EX
            }

            var discountedPrice = item.Price.Subtract(DiscountValue);
            IncrementUsesCount();
            return new Currency(discountedPrice);
        }
    }
}
