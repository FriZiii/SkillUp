using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.DTO
{
    internal class ItemDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public ItemType Type { get; set; }
        public decimal Price { get; set; }
    }
}
