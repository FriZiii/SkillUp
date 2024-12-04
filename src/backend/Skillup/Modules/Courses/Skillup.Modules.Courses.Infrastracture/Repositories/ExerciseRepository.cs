using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class ExerciseRepository : IExerciseRepository
    {

        private readonly CoursesDbContext _context;
        private readonly DbSet<QuestionAnswer> _questions;
        private readonly DbSet<QuizQuestion> _quizQuestions;
        private readonly DbSet<QuizAnswer> _quizAnswers;

        public ExerciseRepository(CoursesDbContext context)
        {
            _context = context;
            _questions = context.QuestionAnswerExercises;
            _quizQuestions = context.QuizQuestionExercises;
            _quizAnswers = context.QuizAnswer;
        }
        public async Task AddQuestionAnswer(QuestionAnswer questionAnswer)
        {
            _questions.Add(questionAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuiz(QuizQuestion quizQuestion)
        {
            _quizQuestions.Add(quizQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuizAnswer(QuizAnswer quizAnswer)
        {
            _quizAnswers.Add(quizAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(Guid exerciseId)
        {
            var exxercise = _questions.FirstOrDefault(e => e.Id == exerciseId) ?? throw new Exception();
            _questions.Remove(exxercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuiz(Guid exerciseId)
        {
            var exxercise = _quizQuestions.FirstOrDefault(e => e.Id == exerciseId) ?? throw new Exception();
            _quizQuestions.Remove(exxercise);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuestionAnswer>> GetQuestionAnswers(Guid assignmentId)
        {
            var questions = await _questions.Where(q => q.AssignmentId == assignmentId)
                .ToListAsync();
            return questions;
        }

        public async Task<List<QuizQuestion>> GetQuizzes(Guid assignmentId)
        {
            var quizzes = await _quizQuestions.Include(q => q.Answers).Where(q => q.AssignmentId == assignmentId)
                .ToListAsync();
            return quizzes;
        }
    }
}
