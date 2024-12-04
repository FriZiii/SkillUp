using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    public class FillTheGapConfiguration : IEntityTypeConfiguration<FillTheGapSentence>
    {
        public void Configure(EntityTypeBuilder<FillTheGapSentence> builder)
        {
            builder.ToTable("FillTheGapSentence")
               .HasBaseType<Exercise>();

            builder.HasMany(s => s.Words)
                .WithOne(w => w.Sentence)
                .HasForeignKey(w => w.SentenceId);
        }
    }
}
