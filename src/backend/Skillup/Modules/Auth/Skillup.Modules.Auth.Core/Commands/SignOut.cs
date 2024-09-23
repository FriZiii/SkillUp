using MediatR;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record SignOut(Guid UserId) : IRequest;
}
