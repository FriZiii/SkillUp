namespace Skillup.Modules.Finances.Core.DTO
{
    internal class ItemEarningsDto
    {
        public Guid ItemId { get; set; }
        public int ItemsCount { get; set; }
        public decimal Total { get; set; }
    }
}
