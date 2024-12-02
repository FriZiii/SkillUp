using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets.Exercises
{
    internal class GetQuizListHandler(IExerciseRepository exerciseRepository) : IRequestHandler<GetQuizListRequest, IEnumerable<QuizQuestionDto>>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

        public async Task<IEnumerable<QuizQuestionDto>> Handle(GetQuizListRequest request, CancellationToken cancellationToken)
        {
            var quizzes = await _exerciseRepository.GetQuizzes(request.AssignmentId);
            var mapper = new ExerciseMapper();
            var quizzesDto = quizzes.Select(mapper.ExerciseToExerciseDto);
            return quizzesDto;
        }
    }
}
