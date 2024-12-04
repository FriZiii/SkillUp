using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise;

namespace Skillup.Modules.Courses.Application.Features.Queries.Assets.Exercises
{
    internal class GetFillTheGapListHandler(IFillTheGapRepository fillTheGapRepository) : IRequestHandler<GetFillTheGapListRequest, IEnumerable<FillTheGapSentenceDto>>
    {
        private readonly IFillTheGapRepository _fillTheGapRepository = fillTheGapRepository;

        public async Task<IEnumerable<FillTheGapSentenceDto>> Handle(GetFillTheGapListRequest request, CancellationToken cancellationToken)
        {
            var fillTheGaps = await _fillTheGapRepository.GetFillTheGaps(request.AssignmentId);

            var mapper = new ExerciseMapper();
            var dtos = fillTheGaps.Select(mapper.ExerciseToExerciseDto);
            return dtos;
        }
    }
}
