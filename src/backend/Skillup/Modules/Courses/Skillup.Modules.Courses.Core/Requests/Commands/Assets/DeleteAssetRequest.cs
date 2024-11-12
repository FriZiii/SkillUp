using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets
{
    public record DeleteAssetRequest(Guid ElementId) : IRequest;
}
