using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}
