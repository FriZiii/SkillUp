using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests
{
    public record EditUserRequest : UserDto, IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    };
}
