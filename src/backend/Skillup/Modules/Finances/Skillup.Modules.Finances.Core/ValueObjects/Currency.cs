namespace Skillup.Modules.Finances.Core.ValueObjects
{
    internal class Currency
    {
        public decimal Amount { get; private set; }

        public Currency(decimal initialAmount)
        {
            if (initialAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialAmount), "Balance cannot be negative.");
            }
            Amount = initialAmount;
        }


        public Currency Add(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to add cannot be negative.");

            return new Currency(Amount + amount);
        }

        public Currency Subtract(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to subtract cannot be negative.");

            if (Amount - amount < 0)
                throw new InvalidOperationException("Insufficient balance.");

            return new Currency(Amount - amount);
        }

        public static implicit operator decimal(Currency currency) => currency.Amount;
        public static implicit operator Currency(decimal currency) => new(currency);
    }
}
