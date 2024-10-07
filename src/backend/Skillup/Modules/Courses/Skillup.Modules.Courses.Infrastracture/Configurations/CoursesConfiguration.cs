using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.OwnsOne(c => c.Info, i =>
            {
                i.Property(x => x.Title).HasColumnName("Title");
                i.Property(x => x.Subtitle).HasColumnName("Subtitle");

            });

            builder.HasOne(c => c.Category)
                .WithMany(category => category.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Subcategory)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SubcategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            var converter = new ValueConverter<StringListValueObject, string>(
                v => string.Join(",", v.Values),
                v => new StringListValueObject(v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            builder.OwnsOne(c => c.Details, d =>
            {
                d.Property(x => x.Description)
                .HasColumnName("Description");
                d.Property(x => x.Level)
                .HasColumnName("Difficulty")
                .HasConversion<string>();
                d.Property(x => x.MustKnowBefore)
                .HasColumnName("MustKnowBefore")
                .HasConversion(converter);
                d.Property(x => x.ObjectivesSummary)
                .HasColumnName("ObjectivesSummary")
                .HasConversion(converter);
                d.Property(x => x.IntendedFor)
                .HasColumnName("IntendedFor")
                .HasConversion(converter);
            });

            builder.Property(c => c.ThumbnailUrl)
                .HasConversion(
                    v => v.ToString(),
                    v => new Uri(v));

            builder.HasMany(c => c.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
