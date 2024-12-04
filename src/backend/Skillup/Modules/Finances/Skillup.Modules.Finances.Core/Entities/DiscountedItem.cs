namespace Skillup.Modules.Finances.Core.Entities
{
    internal class DiscountedItem
    {
        public DiscountedItem(Guid discountCodeId, Guid itemId)
        {
            DiscountCodeId = discountCodeId;
            ItemId = itemId;
        }

        public Guid Id { get; set; }
        public Guid DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
