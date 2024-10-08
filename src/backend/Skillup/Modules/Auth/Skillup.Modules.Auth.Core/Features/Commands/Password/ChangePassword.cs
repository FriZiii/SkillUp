using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Commands.Password
{
    internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : IRequest;
}
