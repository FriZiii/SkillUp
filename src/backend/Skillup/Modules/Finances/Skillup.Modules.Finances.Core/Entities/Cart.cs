using Skillup.Modules.Finances.Core.ValueObjects;

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
                throw new InvalidOperationException("The discount code cannot be applied to this cart."); // TODO: Custom Ex

            DiscountCode = discountCode;
            DiscountCodeId = discountCode.Id;

            if (discountCode.AppliesToEntireCart)
            {
                foreach (var cartItem in Items)
                {
                    var itemPriceBeforeDiscount = cartItem.Item.Price;

                    cartItem.Price = discountCode.Type switch
                    {
                        DiscountCodeType.Percentage => itemPriceBeforeDiscount * (1 - discountCode.DiscountValue / 100),
                        DiscountCodeType.FixedAmount => Math.Max(0, itemPriceBeforeDiscount - discountCode.DiscountValue),
                        _ => throw new InvalidOperationException("Unknown discount code type.") // TODO: Custom Ex
                    };
                }

                Total = Items.Sum(x => x.Price);
            }
            else
            {
                foreach (var cartItem in Items)
                {
                    if (discountCode.DiscountedItems.Any(discountedItem => discountedItem.ItemId == cartItem.ItemId))
                    {
                        var itemPriceBeforeDiscount = cartItem.Item.Price;
                        cartItem.Price = discountCode.Type switch
                        {
                            DiscountCodeType.Percentage => itemPriceBeforeDiscount * (1 - discountCode.DiscountValue / 100),
                            DiscountCodeType.FixedAmount => Math.Max(0, itemPriceBeforeDiscount - discountCode.DiscountValue),
                            _ => throw new InvalidOperationException("Unknown discount code type.") // TODO: Custom Ex
                        };
                    }
                }

                Total = Items.Sum(x => x.Price);
            }
        }
    }
}
