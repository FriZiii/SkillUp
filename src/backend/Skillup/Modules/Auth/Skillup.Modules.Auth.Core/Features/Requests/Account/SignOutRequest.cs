using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record SignOutRequest(Guid UserId) : IRequest;
}
