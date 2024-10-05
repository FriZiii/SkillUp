using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Entities.CourseContent;
using Skillup.Modules.Courses.Infrastracture.Configurations;

namespace Skillup.Modules.Courses.Infrastracture
{
    internal class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Element> Elements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("courses");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.Entity<Section>().HasMany(s => s.Elements).WithOne(e => e.Section).HasForeignKey(e => e.SectionId);
            modelBuilder.Entity<Element>().HasOne(e => e.Asset).WithOne(a => a.Element).HasForeignKey<Element>(e => e.AssetId);

        }
    }
}
