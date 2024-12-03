using Skillup.Modules.Finances.Core.Entities;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Finances.Core.DTO
{
    internal class DiscountCodeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DiscountCodeType Type { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool AppliesToEntireCart { get; set; } = true;
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }

        public IEnumerable<Item>? DiscountedItems { get; set; }
    }

    internal class AppliedDiscountCodeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DiscountCodeType Type { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
    }

    internal class AddDiscountCodeDto
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonIgnore]
        public DiscountCodeType Type { get; set; }

        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool AppliesToEntireCart { get; set; } = true;
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
    }
}
