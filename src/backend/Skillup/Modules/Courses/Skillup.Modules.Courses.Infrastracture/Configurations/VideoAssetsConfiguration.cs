using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class VideoAssetsConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("VideoAssets")
                     .HasBaseType<Asset>();
        }
    }
}
