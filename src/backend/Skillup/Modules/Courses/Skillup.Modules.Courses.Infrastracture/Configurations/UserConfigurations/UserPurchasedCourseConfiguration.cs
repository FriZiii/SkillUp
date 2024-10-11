using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.UserConfigurations
{
    internal class UserPurchasedCourseConfiguration : IEntityTypeConfiguration<UserPurchasedCourse>
    {
        public void Configure(EntityTypeBuilder<UserPurchasedCourse> builder)
        {
            builder.HasKey(pc => new { pc.UserId, pc.CourseId });

            builder.HasOne<User>()
                .WithMany(u => u.PurchasedCourses)
                .HasForeignKey(pc => pc.UserId);

            builder.HasOne<Course>()
                .WithMany()
                .HasForeignKey(pc => pc.CourseId);

            builder.HasIndex(pc => new { pc.UserId, pc.CourseId })
                .IsUnique();
        }
    }
}
