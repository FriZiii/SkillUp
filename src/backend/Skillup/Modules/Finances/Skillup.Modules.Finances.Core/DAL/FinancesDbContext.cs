using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.DAL
{
    internal class FinancesDbContext(DbContextOptions<FinancesDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<DiscountedItem> DiscountedItems { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("finances");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
