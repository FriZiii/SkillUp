using MediatR;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record SignOut(Guid UserId) : IRequest;
}
