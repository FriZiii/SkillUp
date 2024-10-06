using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class AssignmentAssetsConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("AssignmentAssets")
               .HasBaseType<Asset>();
            builder.HasMany(a => a.Exercises)
                .WithOne(e => e.Assignment)
                .HasForeignKey(e => e.AssignmentId);
        }
    }
}
