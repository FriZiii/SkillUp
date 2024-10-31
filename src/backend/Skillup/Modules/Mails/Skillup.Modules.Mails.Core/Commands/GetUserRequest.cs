using MediatR;
using Skillup.Modules.Mails.Core.DTO;

namespace Skillup.Modules.Mails.Core.Commands
{
    public record GetUserRequest(Guid UserId) : IRequest<UserDto>;
}
