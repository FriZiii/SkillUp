using MediatR;
using Skillup.Modules.Courses.Core.DTO.User;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetUserByIdRequest(Guid UserId, bool Details = false) : IRequest<UserDto>;
}
