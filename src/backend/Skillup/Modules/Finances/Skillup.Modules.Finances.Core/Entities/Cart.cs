namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Cart
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
