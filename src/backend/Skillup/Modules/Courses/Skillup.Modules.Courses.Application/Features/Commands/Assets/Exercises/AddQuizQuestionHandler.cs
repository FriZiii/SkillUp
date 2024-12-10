using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class AddQuizQuestionHandler(IExerciseRepository exerciseRepository, IAssetsRepository assetsRepository) : IRequestHandler<AddQuizQuestionRequest, QuizQuestionDto>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;
        private readonly IAssetsRepository _assetsRepository = assetsRepository;

        public async Task<QuizQuestionDto> Handle(AddQuizQuestionRequest request, CancellationToken cancellationToken)
        {
            var assignment = await _assetsRepository.GetAssignmentById(request.AssignmentId);
            if (assignment.ExerciseType != Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.ExerciseType.Quiz)
            {
                throw new Exception();  //TODO: wrong exercise type
            }
            var exercise = new QuizQuestion()
            {
                AssignmentId = request.AssignmentId,
                Question = request.Question,
                Answers = request.Answers.Select((answer, index) => new QuizAnswer
                {
                    Answer = answer,
                    IsCorrectAnswer = request.Correct[index],
                }).ToList()
            };
            await _exerciseRepository.AddQuiz(exercise);

            var quiz = (await _exerciseRepository.GetQuizzes(request.AssignmentId)).FirstOrDefault(q => q.Id == exercise.Id);
            var mapper = new ExerciseMapper();
            return mapper.ExerciseToExerciseDto(quiz!);
        }
    }
}
