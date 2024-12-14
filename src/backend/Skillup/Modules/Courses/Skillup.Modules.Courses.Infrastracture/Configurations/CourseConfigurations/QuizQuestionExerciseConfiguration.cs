using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Infrastracture.Configurations.CourseConfigurations
{
    public class QuizQuestionExerciseConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestionExercises")
               .HasBaseType<Exercise>();

            builder.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}
