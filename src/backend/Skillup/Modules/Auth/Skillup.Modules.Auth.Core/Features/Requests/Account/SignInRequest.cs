using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record SignInRequest(string Email, string Password) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
