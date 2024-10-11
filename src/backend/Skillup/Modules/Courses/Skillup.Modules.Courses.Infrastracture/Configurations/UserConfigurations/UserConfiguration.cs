using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.UserConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.ToString(),
                    emailString => new Email(emailString))
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.OwnsOne(u => u.Details, details =>
            {
                details.Property(d => d.Title).HasColumnName("Title");
                details.Property(d => d.Biography).HasColumnName("Biography");
            });

            builder.OwnsOne(u => u.SocialMediaLinks, links =>
            {
                links.Property(s => s.Website).HasColumnName("Website");
                links.Property(s => s.Twitter).HasColumnName("Twitter");
                links.Property(s => s.Facebook).HasColumnName("Facebook");
                links.Property(s => s.LinkedIn).HasColumnName("LinkedIn");
                links.Property(s => s.YouTube).HasColumnName("YouTube");
            });

            builder.OwnsOne(u => u.PrivacySettings, settings =>
            {
                settings.Property(p => p.IsAccountPublicForLoggedInUsers)
                    .HasColumnName("IsAccountPublicForLoggedInUsers");
                settings.Property(p => p.ShowCoursesOnUserProfile)
                    .HasColumnName("ShowCoursesOnUserProfile");
            });

            builder.HasMany(u => u.PurchasedCourses)
                .WithOne()
                .HasForeignKey(pc => pc.UserId);

            builder.HasIndex(u => u.Id)
                .IsUnique();
        }
    }
}
