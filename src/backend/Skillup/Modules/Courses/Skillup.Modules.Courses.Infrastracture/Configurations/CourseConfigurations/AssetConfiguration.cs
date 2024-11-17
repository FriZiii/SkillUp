using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.HasOne(a => a.Element)
                .WithOne(e => e.Asset)
                .HasForeignKey<Asset>(a => a.ElementId);
        }
    }
}
