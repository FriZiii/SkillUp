using MediatR;
using Skillup.Modules.Auth.Core.DTO;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Commands.Token
{
    internal record Refresh(string? AccessToken, string? RefreshToken) : TokensDto(AccessToken, RefreshToken), IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
