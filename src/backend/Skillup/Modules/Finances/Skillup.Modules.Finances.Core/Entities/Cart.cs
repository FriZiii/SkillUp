using Skillup.Modules.Finances.Core.ValueObjects;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Cart
    {
        public Guid Id { get; set; }
        public Currency Total { get; set; }
        public Guid? DiscountCodeId { get; set; }
        public DiscountCode? DiscountCode { get; set; }
        public List<CartItem> Items { get; set; }

        public void ApplyDiscountCode(DiscountCode discountCode)
        {
            if (!discountCode.CanBeUsed(this))
                throw new BadRequestException("The discount code cannot be applied to this cart.");

            DiscountCode = discountCode;
            DiscountCodeId = discountCode.Id;

            discountCode.ApplyDisountOnCart(this);
        }
    }
}
