using Microsoft.IdentityModel.Tokens;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.ValueObjects;

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

        public bool CanBeUsed()
        {
            return !HasUsageLimit || (UsageCount < MaxUsageLimit);
        }

        public void IncrementUsesCount()
        {
            if (CanBeUsed())
            {
                UsageCount++;
            }
        }

        public abstract Currency Apply(Item item);

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
