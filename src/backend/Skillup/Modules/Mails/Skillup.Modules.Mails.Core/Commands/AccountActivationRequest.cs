using MediatR;

namespace Skillup.Modules.Mails.Core.Commands
{
    internal record AccountActivationRequest(Guid UserId, string Email, Guid ActivationToken, DateTime TokenExpiration) : IRequest;
}
