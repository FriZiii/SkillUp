using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise
{
    public record GetFillTheGapListRequest(Guid AssignmentId) : IRequest<IEnumerable<FillTheGapSentenceDto>>;
}
