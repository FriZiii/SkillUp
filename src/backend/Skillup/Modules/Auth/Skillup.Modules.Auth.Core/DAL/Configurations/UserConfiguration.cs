using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Auth.Core.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100)
                .HasConversion(x => x.Value, x => new Email(x));
            builder.Property(x => x.Password).IsRequired().HasMaxLength(500);
        }
    }
}
