namespace Skillup.Modules.Finances.Core.Entities
{
    internal class BalanceHistory
    {
        public Guid Id { get; private set; }
        public Guid WalletId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Type { get; set; }

        public Wallet Wallet { get; private set; }

        public BalanceHistory(Guid walletId, decimal amount)
        {
            Id = Guid.NewGuid();
            WalletId = walletId;
            Amount = amount;
            Date = DateTime.UtcNow;
        }

        private BalanceHistory() { } // Dla EF Core
    }
}
