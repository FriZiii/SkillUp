using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DTO
{
    internal class OrderItemDto
    {
        public Guid ItemId { get; set; }
        public Currency ItemPrice { get; set; }
    }
}
