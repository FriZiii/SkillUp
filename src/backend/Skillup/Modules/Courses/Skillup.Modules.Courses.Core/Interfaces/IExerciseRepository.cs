using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IExerciseRepository
    {
        Task AddQuestionAnswer(QuestionAnswer questionAnswer);
        Task AddQuiz(QuizQuestion quizQuestion);
        Task AddQuizAnswer(QuizAnswer quizAnswer);
        //Task EditQuestionAnswer(QuestionAnswer questionAnswer);
        //Task Delete(Guid exerciseId);
    }
}
