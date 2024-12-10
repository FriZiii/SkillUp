using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record CreateUserRequest(Guid UserId) : IRequest;
}
