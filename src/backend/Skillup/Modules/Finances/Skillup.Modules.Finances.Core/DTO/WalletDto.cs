namespace Skillup.Modules.Finances.Core.DTO
{
    internal class WalletDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
    }

    internal class WalletWithBalanceHistoryDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }

        public IEnumerable<BalanceHistoryDto> BalanceHistory { get; set; } = Enumerable.Empty<BalanceHistoryDto>();
    }

    internal class BalanceHistoryDto
    {
        public BalanceHistoryDto(Guid id, decimal amount, DateTime date, string title, string type)
        {
            Id = id;
            Amount = amount;
            Date = date;
            Title = title;
            Type = type;
        }

        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Title { get; private set; }
        public string Type { get; private set; }
    }
}
