using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class ArticleAssetsConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("ArticleAssets")
               .HasBaseType<Asset>();
        }
    }
}
