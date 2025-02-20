﻿using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IExerciseRepository
    {
        Task AddQuestionAnswer(QuestionAnswer questionAnswer);
        Task AddQuiz(QuizQuestion quizQuestion);
        Task<List<QuizQuestion>> GetQuizzes(Guid assignmentId);
        Task<List<QuestionAnswer>> GetQuestionAnswers(Guid assignmentId);
        Task DeleteQuiz(Guid exerciseId);
        Task DeleteQuestion(Guid exerciseId);
    }
}
