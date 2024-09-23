using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Commands
{
    internal record SignUp([Required][EmailAddress] string Email, [Required] string Password, string Role) : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; init; } = Guid.NewGuid();
    }
}
