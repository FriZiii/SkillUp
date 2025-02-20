﻿using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.ValueObjects
{
    internal class PercentageDiscountCode : DiscountCode
    {
        public PercentageDiscountCode(AddDiscountCodeDto dto)
            : base(dto)
        {
            if (dto.DiscountValue <= 0 || dto.DiscountValue > 100)
                throw new BadRequestException("Percentage can not be less then 0 and grather then 100");
        }

        public override void ApplyDisountOnCart(Cart cart)
        {
            base.ApplyDisountOnCart(cart);

            if (AppliesToEntireCart)
            {
                var totalBeforeDiscount = cart.Items.Sum(x => x.Price);

                cart.Total = totalBeforeDiscount * (1 - DiscountValue / 100);
            }
            else
            {
                foreach (var cartItem in cart.Items)
                {
                    if (DiscountedItems.Any(discountedItem => discountedItem.ItemId == cartItem.ItemId))
                    {
                        var itemPriceBeforeDiscount = cartItem.Item.Price;
                        cartItem.Price = itemPriceBeforeDiscount * (1 - DiscountValue / 100);
                    }
                }

                cart.Total = cart.Items.Sum(x => x.Price);
            }
        }

        private PercentageDiscountCode() { } // Only for Ef core
    }

    internal class FixedAmountDiscountCode : DiscountCode
    {
        public FixedAmountDiscountCode(AddDiscountCodeDto dto)
            : base(dto)
        {
        }

        public override void ApplyDisountOnCart(Cart cart)
        {
            base.ApplyDisountOnCart(cart);

            if (AppliesToEntireCart)
            {
                var totalBeforeDiscount = cart.Items.Sum(x => x.Item.Price);
                cart.Total = Math.Max(0, totalBeforeDiscount - DiscountValue);
            }
            else
            {
                foreach (var cartItem in cart.Items)
                {
                    if (DiscountedItems.Any(discountedItem => discountedItem.ItemId == cartItem.ItemId))
                    {
                        var itemPriceBeforeDiscount = cartItem.Item.Price;
                        cartItem.Price = Math.Max(0, itemPriceBeforeDiscount - DiscountValue);
                    }
                }

                cart.Total = cart.Items.Sum(x => x.Price);
            }
        }

        private FixedAmountDiscountCode() { } // Only for Ef core
    }
}
