using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Requests.Password
{
    internal record ChangePasswordRequest(Guid UserId, string CurrentPassword, string NewPassword) : IRequest;
}
