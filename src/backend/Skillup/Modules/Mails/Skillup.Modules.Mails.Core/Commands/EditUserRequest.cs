using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Commands
{
    public record EditUserRequest : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool AllowMarketingEmails { get; set; }
    };
}
