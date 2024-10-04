using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Infrastracture
{
    internal class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("courses");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Course>()
                .OwnsOne(c => c.Info);
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Category)
                .WithMany(category => category.Courses)
                .HasForeignKey(c => c.Id);
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Subcategory)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.Id);
            modelBuilder.Entity<Course>()
                .Property(c => c.Level)
                .HasConversion<string>();
        }
    }
}
