using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Assets.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class ExerciseSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Assignment> _assignments;

        public ExerciseSeeder(CoursesDbContext context)
        {
            _context = context;
            _assignments = context.Assignments;
        }
        public async Task Seed()
        {
            var assignment1 = await _assignments.FirstOrDefaultAsync(a => a.Element.Title == "Everyday Vocabulary: Food, Weather, Shopping");
            var assignment2 = await _assignments.FirstOrDefaultAsync(a => a.Element.Title == "Practising Present Simple");

            var questionsToAdd = new List<QuestionAnswer>()
            {
                new QuestionAnswer()
                {
                    Question = " I ________ (go) shopping with my brother. ",
                    CorrectAnswer = " I go shopping with my brother. ",
                    AssignmentId = assignment2.Id
                },
                new QuestionAnswer()
                {
                    Question = "She ________ (do) her homework before dinner.",
                    CorrectAnswer = "She does her homework before dinner.",
                    AssignmentId = assignment2.Id
                },
                new QuestionAnswer()
                {
                    Question = "We ________ (play) tennis in school on Wednesday afternoon. ",
                    CorrectAnswer = "We play tennis in school on Wednesday afternoon. ",
                    AssignmentId = assignment2.Id
                },
                new QuestionAnswer()
                {
                    Question = "School ________ (not / finish) at two o´clock.",
                    CorrectAnswer = "School doesn't finish at two o´clock.",
                    AssignmentId = assignment2.Id
                },
            };
            var quizesToAdd = new List<QuizQuestion>()
            {
                new QuizQuestion()
                {
                    AssignmentId = assignment1.Id,
                    Question = "Which of the following is a type of fruit?",
                },
                new QuizQuestion()
                {
                    AssignmentId = assignment1.Id,
                    Question = "What do you call light rain that falls for a short period of time?",
                },
                new QuizQuestion()
                {
                    AssignmentId = assignment1.Id,
                    Question = "What is the opposite of “sunny” weather?",
                },
            };
            await _context.QuestionAnswerExercises.AddRangeAsync(questionsToAdd);
            await _context.QuizQuestionExercises.AddRangeAsync(quizesToAdd);
            await _context.SaveChangesAsync();

            
            var quizes = _context.QuizQuestionExercises;
            var answerToAdd = new List<QuizAnswer>()
            {
                new QuizAnswer()
                {
                    Answer = "Chicken",
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "Which of the following is a type of fruit?")).Id,
                },
                new QuizAnswer()
                {
                    Answer = "Carrot",
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "Which of the following is a type of fruit?")).Id,
                },
                new QuizAnswer()
                {
                    Answer = "Apple",
                    isCorrectAnswer = true,
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "Which of the following is a type of fruit?")).Id,
                },
                new QuizAnswer()
                {
                    Answer = "Bread",
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "Which of the following is a type of fruit?")).Id,
                },
                new QuizAnswer()
                {
                    Answer = "Thunderstorm",
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What do you call light rain that falls for a short period of time?")).Id,
                },                                                                   
                                                                                     
                new QuizAnswer()                                                     
                {                                                                    
                    Answer = "Drizzle",
                    isCorrectAnswer = true,
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What do you call light rain that falls for a short period of time?")).Id,
                },                                                                  
                new QuizAnswer()                                                    
                {                                                                   
                    Answer = "Hail",                                              
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What do you call light rain that falls for a short period of time?")).Id,
                },                                                                     
                new QuizAnswer()                                                       
                {                                                                      
                    Answer = "Snow",                                                
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What do you call light rain that falls for a short period of time?")).Id,
                },
                new QuizAnswer()
                {
                    Answer = "Cloudy ",
                    isCorrectAnswer = true,
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What is the opposite of “sunny” weather?")).Id,
                },                                                                   
                                                                                     
                new QuizAnswer()                                                     
                {                                                                    
                    Answer = "Windy",                                              
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What is the opposite of “sunny” weather?")).Id,
                },                                                                    
                new QuizAnswer()                                                      
                {                                                                     
                    Answer = "Warm",                                                
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What is the opposite of “sunny” weather?")).Id,
                },                                                                    
                new QuizAnswer()                                                      
                {                                                                     
                    Answer = "Dry",                                                    
                    QuestionId = (await quizes.FirstOrDefaultAsync(q => q.Question == "What is the opposite of “sunny” weather?")).Id,
                },
            };
            await _context.QuizAnswer.AddRangeAsync(answerToAdd);
            await _context.SaveChangesAsync();

        }
    }
}
