using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class SectionsConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasMany(s => s.Elements)
                .WithOne(e => e.Section)
                .HasForeignKey(e => e.SectionId);
        }
    }
}
