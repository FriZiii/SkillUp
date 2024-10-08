using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class AssetsConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.UseTpcMappingStrategy();
        }
    }
}
