using MassTransit.Initializers;
using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets.Exercises
{
    internal class GetQuestionAnswerListHandler(IExerciseRepository exerciseRepository) : IRequestHandler<GetQuestionAnswerListRequest, IEnumerable<QuestionAnswerDto>>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

        public async Task<IEnumerable<QuestionAnswerDto>> Handle(GetQuestionAnswerListRequest request, CancellationToken cancellationToken)
        {
            var questions = await _exerciseRepository.GetQuestionAnswers(request.AssignmentId);
            var mapper = new ExerciseMapper();
            var questionsDto = questions.Select(mapper.ExerciseToExerciseDto);
            return questionsDto;
        }
    }
}
