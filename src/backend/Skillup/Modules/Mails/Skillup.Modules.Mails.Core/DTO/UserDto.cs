using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.DTO
{
    internal class UserDto
    {
        public Guid Id { get; set; }
        public required Email Email { get; set; }
        public bool AllowMarketingEmails { get; set; }
    }
}
