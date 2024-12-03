using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper(
        IncludedMembers = MemberVisibility.All,
        IncludedConstructors = MemberVisibility.All)]
    internal partial class DiscountCodeMapper
    {
        public DiscountCodeDto DiscountCodeToDto(DiscountCode discountCode)
        {
            return new DiscountCodeDto()
            {
                Id = discountCode.Id,
                DiscountValue = discountCode.DiscountValue,
                Code = discountCode.Code,
                IsActive = discountCode.IsActive,
                HasUsageLimit = discountCode.HasUsageLimit,
                IsPublic = discountCode.IsPublic,
                MaxUsageLimit = discountCode.MaxUsageLimit,
                Type = discountCode.Type,
                UsageCount = discountCode.UsageCount,
                AppliesToEntireCart = discountCode.AppliesToEntireCart,
                DiscountedItems = !discountCode.AppliesToEntireCart ? discountCode.DiscountedItems.Select(x => x.Item).ToList() : null,
            };
        }

        public AppliedDiscountCodeDto DiscountCodeToAppliedDto(DiscountCode discountCode)
        {
            return new AppliedDiscountCodeDto()
            {
                Id = discountCode.Id,
                DiscountValue = discountCode.DiscountValue,
                Code = discountCode.Code,
                Type = discountCode.Type,
            };
        }

        public DiscountCode AddDiscountCodeDtoToDiscountCode(AddDiscountCodeDto discountCode)
        {
            if (discountCode.Type == DiscountCodeType.Percentage)
            {
                return new PercentageDiscountCode(discountCode);
            }
            else if (discountCode.Type == DiscountCodeType.FixedAmount)
            {
                return new FixedAmountDiscountCode(discountCode);
            }

            throw new Exception(); // TODO: Custom Exception
        }
    }
}
