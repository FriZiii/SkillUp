using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class AddQuizQuestionHandler(IExerciseRepository exerciseRepository) : IRequestHandler<AddQuizQuestionRequest, QuizQuestionDto>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

        public async Task<QuizQuestionDto> Handle(AddQuizQuestionRequest request, CancellationToken cancellationToken)
        {
            var exercise = new QuizQuestion()
            {
                AssignmentId = request.AssignmentId,
                Question = request.Question
            };
            await _exerciseRepository.AddQuiz(exercise);
            var mapper = new ExerciseMapper();
            return mapper.QuizToQuizDto(exercise);
        }
    }
}
