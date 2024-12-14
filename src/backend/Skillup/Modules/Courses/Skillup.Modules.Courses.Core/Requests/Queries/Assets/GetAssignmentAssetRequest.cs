using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets;

namespace Skillup.Modules.Courses.Core.Requests.Queries.Assets
{
    public record GetAssignmentAssetRequest(Guid ElementId) : IRequest<AssignmentAssetDto>;
}
