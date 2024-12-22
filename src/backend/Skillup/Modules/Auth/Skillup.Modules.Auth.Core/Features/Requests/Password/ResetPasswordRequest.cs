using MediatR;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Auth.Core.Features.Requests.Password
{
    public record ResetPasswordRequest(Email Email) : IRequest;
}
