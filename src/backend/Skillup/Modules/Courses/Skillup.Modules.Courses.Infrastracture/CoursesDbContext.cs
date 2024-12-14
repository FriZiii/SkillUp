using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Infrastracture
{
    internal class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Attachment> ElementAttachments { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Assignment> Assignments { get; set; }


        public DbSet<QuestionAnswer> QuestionAnswerExercises { get; set; }
        public DbSet<QuizQuestion> QuizQuestionExercises { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        public DbSet<FillTheGapSentence> FillTheGapSentences { get; set; }
        public DbSet<FillTheGapWord> FillTheGapWords { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<UserPurchasedCourse> UsersPurchasedCourses { get; set; }

        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<CourseReviewComment> ReviewComments { get; set; }

        public DbSet<CourseRating> CourseRatings { get; set; }

        public DbSet<CourseUserProgess> CourseUserProgess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("courses");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
