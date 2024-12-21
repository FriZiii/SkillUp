using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    internal class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasOne(x => x.Comment)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.CommentId);

            builder.HasOne(x => x.Liker)
                .WithMany()
                .HasForeignKey(x => x.LikerId);
        }
    }
}
