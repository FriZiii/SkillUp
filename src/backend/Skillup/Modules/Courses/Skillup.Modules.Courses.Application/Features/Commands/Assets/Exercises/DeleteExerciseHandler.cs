using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class DeleteExerciseHandler(IExerciseRepository exerciseRepository, IFillTheGapRepository fillTheGapRepository) : IRequestHandler<DeleteExerciseRequest>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;
        private readonly IFillTheGapRepository _fillTheGapRepository = fillTheGapRepository;

        public async Task Handle(DeleteExerciseRequest request, CancellationToken cancellationToken)
        {
            switch (request.ExerciseType)
            {
                case ExerciseType.QuestionAnswer:
                    await _exerciseRepository.DeleteQuestion(request.ExerciseId);
                    break;
                case ExerciseType.Quiz:
                    await _exerciseRepository.DeleteQuiz(request.ExerciseId);
                    break;
                case ExerciseType.FillTheGap:
                    await _fillTheGapRepository.DeleteSentence(request.ExerciseId);
                    break;
            }
        }
    }
}
