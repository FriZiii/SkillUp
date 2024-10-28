namespace Skillup.Modules.Finances.Core.Seeders.Data
{
    internal class ItemJsonModel
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string ItemType { get; set; }
        public decimal Price { get; set; }
    }
}
