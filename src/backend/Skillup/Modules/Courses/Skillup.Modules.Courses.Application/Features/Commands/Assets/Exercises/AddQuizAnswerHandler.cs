using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class AddQuizAnswerHandler(IExerciseRepository exerciseRepository) : IRequestHandler<AddQuizAnswerRequest, QuizAnswerDto>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

        public async Task<QuizAnswerDto> Handle(AddQuizAnswerRequest request, CancellationToken cancellationToken)
        {
            var answer = new QuizAnswer()
            {
                QuestionId = request.QuizId,
                Answer = request.Answer,
                isCorrectAnswer = request.IsCorrect
            };
            await _exerciseRepository.AddQuizAnswer(answer);
            var mapper = new ExerciseMapper();
            return mapper.QuizAnswerToQuizAnswerDto(answer);
        }
    }
}
