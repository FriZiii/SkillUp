using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Wallet
    {
        public Guid Id { get; private set; }
        public Currency Balance { get; private set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public Wallet(Wallet wallet)
        {
            Id = wallet.Id;
            Balance = wallet.Balance;
        }

        public Wallet(User owner)
        {
            Id = Guid.NewGuid();
            Balance = new(0);
            OwnerId = owner.Id;
            Owner = owner;
        }

        public void AddToBalance(decimal amount)
        {
            Balance = Balance.Add(amount);
        }

        public void SubtractFromBalance(decimal amount)
        {
            Balance = Balance.Subtract(amount);
        }

        private Wallet() { } // Used only by EF Core
    }
}
