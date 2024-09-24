using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record SignIn(string Email, string Password) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
