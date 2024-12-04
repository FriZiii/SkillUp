using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DAL.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.ItemPrice)
                .IsRequired()
                .HasConversion(x => x.Amount, x => new Currency(x))
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
