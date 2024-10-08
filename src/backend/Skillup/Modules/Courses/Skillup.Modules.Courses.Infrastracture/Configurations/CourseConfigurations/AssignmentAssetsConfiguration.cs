using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
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
