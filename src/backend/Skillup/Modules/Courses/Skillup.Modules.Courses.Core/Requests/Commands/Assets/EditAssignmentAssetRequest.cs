using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record EditAssignmentAssetRequest(Guid ElementId, string Instruction) : IRequest
    {
    }
}
