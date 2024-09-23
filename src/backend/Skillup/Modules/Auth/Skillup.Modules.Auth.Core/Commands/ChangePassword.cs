using MediatR;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : IRequest;
}
