using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class CartMapper
    {
        public CartDto CartToDto(Cart cart)
        {
            var cartItemMapper = new CartItemMapper();
            var cartItems = cart.Items.Select(cartItemMapper.CartItemToDto);

            AppliedDiscountCodeDto? discountCode = null;
            if (cart.DiscountCode is not null)
            {
                var discountCodeMapper = new DiscountCodeMapper();
                discountCode = discountCodeMapper.DiscountCodeToAppliedDto(cart.DiscountCode);
            }

            return new CartDto()
            {
                Id = cart.Id,
                Total = cart.Total,
                DiscountCode = discountCode,
                TotalBeforeDiscount = discountCode == null ? null : cart.Items.Sum(x => x.Item.Price),
                Items = cartItems
            };
        }
    }
}
