using MediatR;
using Skillup.Modules.Mails.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Commands
{

    public record GetUserRequest(Guid UserId) : IRequest<UserDto>;
}
