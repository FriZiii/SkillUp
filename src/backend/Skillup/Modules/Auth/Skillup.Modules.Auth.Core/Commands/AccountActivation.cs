using MediatR;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record AccountActivation(Guid UserId, Guid ActivationToken) : IRequest;
}
