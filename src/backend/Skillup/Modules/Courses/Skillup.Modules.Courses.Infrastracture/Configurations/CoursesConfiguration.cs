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
            builder.OwnsOne(c => c.Info);

            builder.HasOne(c => c.Category)
                .WithMany(category => category.Courses)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Subcategory)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Level)
                .HasConversion<string>();

            var converter = new ValueConverter<StringList, string>(
                v => string.Join(",", v.Values),
                v => new StringList(v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            builder.Property(c => c.ObjectivesSummary)
                .HasConversion(converter);

            builder.Property(c => c.MustKnowBefore)
                .HasConversion(converter);

            builder.Property(c => c.IntendedFor)
                .HasConversion(converter);

            builder.Property(c => c.ThumbnailUrl)
                .HasConversion(
                    v => v.ToString(),
                    v => new Uri(v));

            builder.OwnsOne(c => c.Price, p =>
            {
                p.Property(pp => pp.Value).HasColumnName("Price").HasColumnType("decimal(18,2)");
            });

            builder.HasMany(c => c.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
