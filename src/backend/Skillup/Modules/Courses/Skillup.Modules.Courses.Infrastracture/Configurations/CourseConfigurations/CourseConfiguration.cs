using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

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

                d.Property(x => x.ThumbnailUrl)
                    .HasColumnName("ThumbnailUrl")
                     .HasConversion(v => v.ToString(), v => new Uri(v));
            });

            builder.HasMany(c => c.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);

            builder.HasOne(c => c.Author)
                .WithMany(a => a.CreatedCoures)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
