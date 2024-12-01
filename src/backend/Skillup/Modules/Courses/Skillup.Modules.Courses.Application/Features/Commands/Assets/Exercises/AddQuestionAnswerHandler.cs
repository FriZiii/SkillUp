using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class AddQuestionAnswerHandler(IExerciseRepository exerciseRepository) : IRequestHandler<AddQuestionAnswerRequest, QuestionAnswerDto>
    {
        private readonly IExerciseRepository _exerciseRepository = exerciseRepository;

        public async Task<QuestionAnswerDto> Handle(AddQuestionAnswerRequest request, CancellationToken cancellationToken)
        {
            var exercise = new QuestionAnswer()
            {
                AssignmentId = request.AssignmentId,
                Question = request.Question,
                CorrectAnswer = request.Answer,
            };
            await _exerciseRepository.AddQuestionAnswer(exercise);
            var mapper = new ExerciseMapper();
            return mapper.QuestionAnswerToQuestionAnsweerDto(exercise);
        }
    }
}
