using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Commands
{
    internal record AddUserRequest(Guid UserID, string Email, bool AllowMarketingEmails) : IRequest;
}
