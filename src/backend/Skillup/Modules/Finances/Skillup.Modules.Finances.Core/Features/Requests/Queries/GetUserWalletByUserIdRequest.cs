using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetUserWalletByUserIdRequest(Guid UserId) : IRequest<Wallet>;
}
