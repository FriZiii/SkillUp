namespace Skillup.Modules.Finances.Core.Entities
{
    internal class CartItem
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public decimal Price { get; set; }
    }
}
