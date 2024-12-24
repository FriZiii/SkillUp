using Skillup.Modules.Finances.Core.Entities;
using System.Globalization;

namespace Skillup.Modules.Finances.Core.DTO
{
    internal class YearEarningsDto
    {
        public YearEarningsDto(int year, IEnumerable<OrderItem> orderItems)
        {
            Year = year;
            Months = DateTimeFormatInfo.CurrentInfo.MonthNames
                              .Where(m => !string.IsNullOrEmpty(m))
                              .ToList();

            MonthlyEarnings = orderItems
                .Where(o => o.Order.Created.Year == year)
                .GroupBy(o => o.ItemId)
                .Select(group => new MonthlyEarningsDto
                {
                    ItemId = group.Key,
                    Data = Enumerable.Range(1, 12)
                        .Select(month => group
                            .Where(o => o.Order.Created.Month == month)
                            .Sum(o => o.ItemPrice.Amount))
                        .ToList()
                })
                .ToList();
        }

        public int Year { get; set; }
        public IEnumerable<string> Months { get; private set; }
        public IEnumerable<MonthlyEarningsDto> MonthlyEarnings { get; set; }
    }

    public class MonthlyEarningsDto
    {
        public Guid ItemId { get; set; }
        public IEnumerable<decimal> Data { get; set; }
    }
}
