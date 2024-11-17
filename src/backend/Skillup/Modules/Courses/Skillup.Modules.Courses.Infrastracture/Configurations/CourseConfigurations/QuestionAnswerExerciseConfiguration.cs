using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    internal class QuestionAnswerExerciseConfiguration : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.ToTable("QuestionAnswerExercise")
               .HasBaseType<Exercise>();
        }
    }
}
