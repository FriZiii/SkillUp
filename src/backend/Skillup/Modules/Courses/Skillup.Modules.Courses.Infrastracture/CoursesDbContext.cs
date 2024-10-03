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
        }
    }
}
