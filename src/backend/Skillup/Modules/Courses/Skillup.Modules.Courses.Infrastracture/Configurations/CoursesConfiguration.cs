using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skillup.Modules.Courses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
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
                v => string.Join(",", v),   // Z listy do stringa
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
            //builder.Property(c => c.Price.Value)
            //    .HasColumnType("decimal(18,2)")
            //    .HasColumnName("Price");


            builder.HasMany(c => c.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
