using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DTO
{
    internal class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Currency TotalPrice { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
