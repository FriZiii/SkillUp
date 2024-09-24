using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record SignUp(string Email, string Password) : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; init; } = Guid.NewGuid();
    }
}
