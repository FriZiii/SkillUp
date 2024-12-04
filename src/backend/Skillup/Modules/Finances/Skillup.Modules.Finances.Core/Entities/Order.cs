using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Order
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Currency TotalPrice { get; set; }
        public Guid OrdererId { get; set; }
        public User Orderer { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
