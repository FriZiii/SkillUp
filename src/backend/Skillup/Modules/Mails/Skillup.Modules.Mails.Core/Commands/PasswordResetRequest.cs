using MediatR;

namespace Skillup.Modules.Mails.Core.Commands
{
    public record PasswordResetRequest(Guid UserId, string Token) : IRequest;
}
