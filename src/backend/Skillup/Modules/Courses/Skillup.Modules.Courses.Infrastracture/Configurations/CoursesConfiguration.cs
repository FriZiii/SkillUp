using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
                .HasForeignKey(c => c.Id);

            builder.HasOne(c => c.Subcategory)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.Id);

            builder.Property(c => c.Level)
                .HasConversion<string>();

            var converter = new ValueConverter<List<string>, string>(
                v => string.Join(",", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            var comparer = new ValueComparer<List<string>>((c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            builder.Property(c => c.ObjectivesSummary)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

            builder.Property(c => c.MustKnowBefore)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

            builder.Property(c => c.IntendedFor)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

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
