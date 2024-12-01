using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record AddAssignmentAssetRequest(Guid ElementId, string Instruction) : IRequest<AssignmentDto>
    {
    }
}
