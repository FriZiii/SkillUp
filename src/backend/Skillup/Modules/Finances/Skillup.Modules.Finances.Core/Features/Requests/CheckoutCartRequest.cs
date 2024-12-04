using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests
{
    internal record CheckoutCartRequest(Guid CartId, Guid WalletId) : IRequest;
}
