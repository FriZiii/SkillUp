using MediatR;
using Skillup.Shared.Abstractions.Auth;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Requests.User
{
    internal record ChangeUserRoleRequest(Guid UserId, UserRole Role) : IRequest
    {
        [JsonIgnore]
        public Guid RequestingUserId { get; set; }
    }
}
