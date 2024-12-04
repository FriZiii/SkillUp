namespace Skillup.Modules.Finances.Core.DTO
{
    internal class WalletDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
    }
}
