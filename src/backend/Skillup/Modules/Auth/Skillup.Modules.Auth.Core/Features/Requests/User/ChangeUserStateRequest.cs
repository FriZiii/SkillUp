using MediatR;
using Skillup.Modules.Auth.Core.Entities;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Requests.User
{
    internal record ChangeUserStateRequest(Guid UserId, UserState State) : IRequest
    {
        [JsonIgnore]
        public Guid RequestingUserId { get; set; }
    }
}
