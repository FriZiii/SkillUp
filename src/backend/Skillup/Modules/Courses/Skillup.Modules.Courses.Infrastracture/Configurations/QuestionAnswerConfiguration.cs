using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.ToTable("QuestionAnswer")
               .HasBaseType<Exercise>();
        }
    }
}
