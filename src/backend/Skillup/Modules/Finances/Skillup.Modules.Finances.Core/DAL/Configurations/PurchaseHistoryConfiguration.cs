using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DAL.Configurations
{
    internal class PurchaseHistoryConfiguration : IEntityTypeConfiguration<PurchaseHistory>
    {
        public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.UserWallet)
                .WithMany()
                .HasForeignKey(x => x.UserWalletId);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasConversion(x => x.Amount, x => new Currency(x))
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId);
        }
    }
}
