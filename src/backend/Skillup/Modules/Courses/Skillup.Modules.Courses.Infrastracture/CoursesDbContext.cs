using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Infrastracture
{
    internal class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswerExercises { get; set; }
        public DbSet<QuizQuestion> QuizQuestionExercises { get; set; }
        public DbSet<QuizAnswer> QuizAnswer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("courses");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
