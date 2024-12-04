using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;

namespace Skillup.Modules.Courses.Application.Features.Commands.Assets.Exercises
{
    internal class AddFillTheGapHandler(IFillTheGapRepository fillTheGapRepository) : IRequestHandler<AddFillTheGapRequest, FillTheGapSentenceDto>
    {
        private readonly IFillTheGapRepository _fillTheGapRepository = fillTheGapRepository;

        public async Task<FillTheGapSentenceDto> Handle(AddFillTheGapRequest request, CancellationToken cancellationToken)
        {
            var sentence = new FillTheGapSentence()
            {
                AssignmentId = request.AssignmentId,
                Sentence = request.Sentence
            };
            await _fillTheGapRepository.AddSentence(sentence);

            var words = new List<FillTheGapWord>();
            foreach (var word in request.Words)
            {
                words.Add(new FillTheGapWord()
                {
                    Value = word.Value,
                    Index = word.Index,
                    SentenceId = sentence.Id,
                });
            }
            await _fillTheGapRepository.AddWords(words);

            var mapper = new ExerciseMapper();
            var dto = mapper.ExerciseToExerciseDto(sentence);
            return dto;
        }
    }
}
