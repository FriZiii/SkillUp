namespace Skillup.Modules.Finances.Core.DTO
{
    internal class CartDto
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}
