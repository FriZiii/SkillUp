using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets.Exercises;
using Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels;
using System.Text.Json;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class ExerciseSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Assignment> _assignments;
        private List<Assignment> _assignmentList = new();
        public List<QuizQuestion> _quizQuestionsList = new();
        private JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
        private List<QuizJsonModel> data = new();

        public ExerciseSeeder(CoursesDbContext context)
        {
            _context = context;
            _assignments = context.Assignments;
        }
        public async Task Seed()
        {
            if (!await _context.QuestionAnswerExercises.AnyAsync() && !await _context.QuizQuestionExercises.AnyAsync() && !await _context.QuizAnswer.AnyAsync())
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

                var jsonString = File.ReadAllText(Path.Combine(path, "quiz-seeder-data.json"));
                data = JsonSerializer.Deserialize<List<QuizJsonModel>>(jsonString, _jsonSerializerOptions);

                _assignmentList = await _assignments.Include(a => a.Element).ToListAsync();
                await _context.QuestionAnswerExercises.AddRangeAsync(CreateQuestions());
                await _context.QuizQuestionExercises.AddRangeAsync(CreateQuizes(data!));
                await _context.SaveChangesAsync();
                _quizQuestionsList = await _context.QuizQuestionExercises.ToListAsync();
                await _context.QuizAnswer.AddRangeAsync(CreateQuizAnswers(data!));
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<QuestionAnswer> CreateQuestions()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "question-seeder-data.json"));
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<QuestionJsonModel>>(jsonString, options);

            return data!.Select(CreateQuestionFromJson);
        }

        private QuestionAnswer CreateQuestionFromJson(QuestionJsonModel jsonModel)
        {
            return new QuestionAnswer()
            {
                AssignmentId = _assignmentList.First(a => a.Element.Title == jsonModel.ElementTitle).Id,
                Question = jsonModel.Question,
                CorrectAnswer = jsonModel.CorrectAnswer
            };
        }

        private IEnumerable<QuizQuestion> CreateQuizes(List<QuizJsonModel> data)
        {
            return data!.Select(CreateQuizFromJson);
        }

        private QuizQuestion CreateQuizFromJson(QuizJsonModel jsonModel)
        {
            return new QuizQuestion()
            {
                AssignmentId = _assignmentList.First(a => a.Element.Title == jsonModel.ElementTitle).Id,
                Question = jsonModel.Question
            };
        }

        public IEnumerable<QuizAnswer> CreateQuizAnswers(List<QuizJsonModel> data)
        {
            var answers = new List<QuizAnswer>();

            foreach (var question in data!)
            {
                foreach (var answer in question.Answers)
                {
                    answers.Add(new QuizAnswer()
                    {
                        Answer = answer.Answer,
                        isCorrectAnswer = answer.IsCorrect,
                        QuestionId = _quizQuestionsList.First(x => x.Question == question.Question).Id,
                    });
                }
            }

            return answers;
        }
    }
}
