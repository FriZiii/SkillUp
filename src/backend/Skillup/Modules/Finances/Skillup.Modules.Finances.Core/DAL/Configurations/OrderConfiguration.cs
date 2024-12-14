using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DAL.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalPrice)
                .IsRequired()
                .HasConversion(x => x.Amount, x => new Currency(x))
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Orderer)
                .WithMany()
                .HasForeignKey(x => x.OrdererId)
                .IsRequired();
        }
    }
}
