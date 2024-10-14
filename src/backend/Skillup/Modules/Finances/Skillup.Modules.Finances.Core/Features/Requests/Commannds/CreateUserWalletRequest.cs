using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record CreateUserWalletRequest(Guid UserId) : IRequest;
}
