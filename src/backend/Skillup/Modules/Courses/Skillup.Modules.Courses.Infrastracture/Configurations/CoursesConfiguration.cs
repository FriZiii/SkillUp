using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
