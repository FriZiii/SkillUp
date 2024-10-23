using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Item
    {
        public Guid Id { get; private set; }
        public Guid AuthorId { get; private set; }
        public ItemType Type { get; private set; }
        public Currency Price { get; set; }

        public Item(Guid itemId, Guid authorId, ItemType itemType, Currency price)
        {
            Id = itemId;
            AuthorId = authorId;
            Type = itemType;
            Price = price;
        }

        private Item() { } // for ef core
    }
}
