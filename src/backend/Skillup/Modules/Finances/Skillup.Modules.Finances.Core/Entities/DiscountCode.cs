using Microsoft.IdentityModel.Tokens;
using Skillup.Modules.Finances.Core.DTO;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal abstract class DiscountCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool AppliesToEntireCart { get; set; } = true;
        public bool HasUsageLimit { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public int? MaxUsageLimit { get; set; }
        public int UsageCount { get; set; }

        public DiscountCodeType Type { get; set; }
        public IEnumerable<DiscountedItem> DiscountedItems { get; set; }

        public ICollection<Cart> Carts { get; set; } = [];

        public bool CanBeUsed(Cart cart)
        {
            if (!IsActive)
                return false;

            if (HasUsageLimit && (UsageCount >= MaxUsageLimit))
                return false;

            if (!AppliesToEntireCart && !DiscountedItems.Any(discountedItem => cart.Items.Any(cartItem => cartItem.ItemId == discountedItem.ItemId)))
                return false;

            return true;
        }

        public virtual void ApplyDisountOnCart(Cart cart)
        {
            if (!CanBeUsed(cart))
                throw new InvalidOperationException("The discount code cannot be applied to this cart."); // TODO: Custom Ex

            cart.DiscountCode = this;
            cart.DiscountCodeId = Id;
        }

        protected DiscountCode(AddDiscountCodeDto dto)
        {
            if (dto.Code.IsNullOrEmpty())
                throw new Exception(); // TODO: Custom Ex

            if (dto.DiscountValue < 0)
                throw new Exception();

            if (dto.HasUsageLimit)
            {
                if (dto.MaxUsageLimit < 0)
                    throw new Exception();
            }
            else
            {
                dto.MaxUsageLimit = 0;
            }

            Id = dto.Id;
            Code = dto.Code;

            DiscountValue = dto.DiscountValue;
            AppliesToEntireCart = dto.AppliesToEntireCart;
            HasUsageLimit = dto.HasUsageLimit;
            IsActive = dto.IsActive;
            IsPublic = dto.IsPublic;
            MaxUsageLimit = dto.MaxUsageLimit;
            UsageCount = dto.UsageCount;
        }

        protected internal DiscountCode() { } // Only for Ef core
    }

    public enum DiscountCodeType
    {
        Percentage, FixedAmount
    }
}
