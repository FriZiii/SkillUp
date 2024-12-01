using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.DAL.Configurations
{
    internal class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Code).IsRequired();
            builder.HasIndex(d => d.Code).IsUnique();

            builder.Property(d => d.DiscountValue)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(d => d.HasUsageLimit).IsRequired();
            builder.Property(d => d.IsActive).IsRequired();
            builder.Property(d => d.IsPublic).IsRequired();
            builder.Property(d => d.MaxUsageLimit);
            builder.Property(d => d.UsageCount)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.HasDiscriminator<DiscountCodeType>("Type")
                   .HasValue<PercentageDiscountCode>(DiscountCodeType.Percentage)
                   .HasValue<FixedAmountDiscountCode>(DiscountCodeType.FixedAmount);

            builder.HasIndex(d => d.IsActive);
            builder.HasIndex(d => d.IsPublic);
        }
    }
}
