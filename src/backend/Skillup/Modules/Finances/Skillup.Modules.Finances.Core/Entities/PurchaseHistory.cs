using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class PurchaseHistory
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid UserWalletId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Currency Price { get; set; }

        // Navigation properties
        public Item Item { get; set; }
        public Wallet UserWallet { get; set; }

        public PurchaseHistory(Guid itemId, Guid userWalletId, DateTime purchaseDate, Currency price)
        {
            ItemId = itemId;
            UserWalletId = userWalletId;
            PurchaseDate = purchaseDate;
            Price = price;
        }
    }
}
