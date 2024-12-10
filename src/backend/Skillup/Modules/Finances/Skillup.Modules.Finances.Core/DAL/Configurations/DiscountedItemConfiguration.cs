using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.DAL.Configurations
{
    internal class DiscountedItemConfiguration : IEntityTypeConfiguration<DiscountedItem>
    {
        public void Configure(EntityTypeBuilder<DiscountedItem> builder)
        {
            builder.HasOne(di => di.DiscountCode)
                .WithMany(x => x.DiscountedItems)
                .HasForeignKey(di => di.DiscountCodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
