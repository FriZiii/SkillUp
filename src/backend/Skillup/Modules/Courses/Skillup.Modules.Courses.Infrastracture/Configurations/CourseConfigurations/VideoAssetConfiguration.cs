using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    internal class VideoAssetConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("VideoAssets")
                     .HasBaseType<Asset>();
        }
    }
}
