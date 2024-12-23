using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Requests.Password
{
    internal record ChangePasswordRequest : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
