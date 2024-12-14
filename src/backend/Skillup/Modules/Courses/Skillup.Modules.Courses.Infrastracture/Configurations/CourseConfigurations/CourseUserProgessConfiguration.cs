using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    internal class CourseUserProgessConfiguration : IEntityTypeConfiguration<CourseUserProgess>
    {
        public void Configure(EntityTypeBuilder<CourseUserProgess> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Element).WithMany().HasForeignKey(x => x.ElementId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
