﻿using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class ExerciseMapper
    {
        public QuestionAnswerDto ExerciseToExerciseDto(QuestionAnswer questionAnswer)
        {
            var questionAnswerDto = new QuestionAnswerDto()
            {
                Id = questionAnswer.Id,
                AssignmentId = questionAnswer.AssignmentId,
                Question = questionAnswer.Question,
                CorrectAnswer = questionAnswer.CorrectAnswer,
            };
            return questionAnswerDto;
        }

        public QuizQuestionDto ExerciseToExerciseDto(QuizQuestion quizQuestion)
        {
            var quizQuestionDto = new QuizQuestionDto()
            {
                Id = quizQuestion.Id,
                AssignmentId = quizQuestion.AssignmentId,
                Question = quizQuestion.Question,
                Answers = quizQuestion.Answers?.Select(QuizAnswerToQuizAnswerDto).ToList(),
            };
            return quizQuestionDto;
        }

        public QuizAnswerDto QuizAnswerToQuizAnswerDto(QuizAnswer quizAnswer)
        {
            var quizAnswerDto = new QuizAnswerDto()
            {
                Id = quizAnswer.Id,
                QuizId = quizAnswer.QuestionId,
                Answer = quizAnswer.Answer,
                IsCorrect = quizAnswer.IsCorrectAnswer,
            };
            return quizAnswerDto;
        }

        public FillTheGapSentenceDto ExerciseToExerciseDto(FillTheGapSentence sentence)
        {
            var fillTheGapSentenceDto = new FillTheGapSentenceDto()
            {
                Id = sentence.Id,
                AssignmentId = sentence.AssignmentId,
                Value = sentence.Sentence,
                Words = sentence.Words?.Select(WordToWordDto).ToList(),
            };
            return fillTheGapSentenceDto;
        }

        public FillTheGapWordDto WordToWordDto(FillTheGapWord word)
        {
            var fillTheGapWordDto = new FillTheGapWordDto()
            {
                Id = word.Id,
                Value = word.Value,
                Index = word.Index,
                SentenceId = word.SentenceId,
            };
            return fillTheGapWordDto;
        }
    }
}
