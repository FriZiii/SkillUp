using Microsoft.EntityFrameworkCore;

namespace Skillup.Modules.Auth.Infrastructure.Ef
{
    internal class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
