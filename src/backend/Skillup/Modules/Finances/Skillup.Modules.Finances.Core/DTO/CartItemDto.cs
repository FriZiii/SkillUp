namespace Skillup.Modules.Finances.Core.DTO
{
    internal class CartItemDto
    {
        public Guid Id { get; set; }
        public ItemDto OrginalItem { get; set; }
        public decimal Price { get; set; }
    }
}
