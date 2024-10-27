using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Mails.Core.Entities;

namespace Skillup.Modules.Mails.Core.DAL
{
    internal class MailDbContext(DbContextOptions<MailDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("mails");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
