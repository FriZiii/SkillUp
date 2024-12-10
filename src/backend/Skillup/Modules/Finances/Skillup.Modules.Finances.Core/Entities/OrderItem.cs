using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Currency ItemPrice { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
