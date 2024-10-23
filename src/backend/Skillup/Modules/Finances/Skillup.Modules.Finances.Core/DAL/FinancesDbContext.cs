using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.DAL
{
    internal class FinancesDbContext(DbContextOptions<FinancesDbContext> options) : DbContext(options)
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("finances");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
