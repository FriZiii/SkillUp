using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record AccountActivationRequest(Guid UserId, Guid ActivationToken) : IRequest;
}
