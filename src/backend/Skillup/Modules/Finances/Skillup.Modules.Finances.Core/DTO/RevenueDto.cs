namespace Skillup.Modules.Finances.Core.DTO
{
    internal class RevenueDto
    {
        public decimal TotalRevenue { get; set; }
        public DateTime BeginDate { get; set; }
        public decimal LastWeekRevenue { get; set; }
        public decimal ChangePercentage { get; set; }
        public int ItemsCovered { get; set; }
    }
}
