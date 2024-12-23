using MediatR;

namespace Skillup.Modules.Mails.Core.Commands
{
    public record class PasswordChangedRequest(Guid UserId) : IRequest;
}
