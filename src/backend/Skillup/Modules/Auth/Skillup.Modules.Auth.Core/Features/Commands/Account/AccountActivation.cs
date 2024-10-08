using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record AccountActivation(Guid UserId, Guid ActivationToken) : IRequest;
}
