using Microsoft.IdentityModel.Tokens;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Infrastructure.Time;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal abstract class DiscountCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? ExpireAt { get; set; }
        public decimal DiscountValue { get; set; }
        public bool AppliesToEntireCart { get; set; } = true;
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public DiscountCodeType Type { get; set; }
        public IEnumerable<DiscountedItem> DiscountedItems { get; set; }

        public bool CanBeUsed(Cart cart)
        {
            if (new UtcClock().CurrentDate() < StartAt || (ExpireAt.HasValue && new UtcClock().CurrentDate() > ExpireAt.Value))
                return false;

            if (!IsActive)
                return false;

            if (!AppliesToEntireCart && !DiscountedItems.Any(discountedItem => cart.Items.Any(cartItem => cartItem.ItemId == discountedItem.ItemId)))
                return false;

            return true;
        }

        public virtual void ApplyDisountOnCart(Cart cart)
        {
            if (!CanBeUsed(cart))
                throw new BadRequestException("The discount code cannot be applied to this cart.");
        }

        protected DiscountCode(AddDiscountCodeDto dto)
        {
            if (dto.Code.IsNullOrEmpty())
                throw new BadRequestException("Code value cannot be empty");

            if (dto.DiscountValue < 0)
                throw new BadRequestException("Discount code value cannot be less then 0");

            Id = dto.Id;
            Code = dto.Code;

            DiscountValue = dto.DiscountValue;
            AppliesToEntireCart = dto.AppliesToEntireCart;
            IsActive = dto.IsActive;
            IsPublic = dto.IsPublic;

            ExpireAt = dto.ExpireAt;
            StartAt = dto.StartAt;
        }

        protected internal DiscountCode() { } // Only for Ef core
    }

    public enum DiscountCodeType
    {
        Percentage, FixedAmount
    }
}
