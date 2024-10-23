using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Entities
{
    internal class Wallet
    {
        public Guid Id { get; private set; }
        public Currency Balance { get; private set; }

        public Wallet(Guid userId)
        {
            Balance = new(0);
            Id = userId;
        }

        public void AddToBalance(decimal amount)
        {
            Balance = Balance.Add(amount);
        }

        public void SubtractFromBalance(decimal amount)
        {
            Balance = Balance.Subtract(amount);
        }

        public Wallet()
        {

        }
    }
}
