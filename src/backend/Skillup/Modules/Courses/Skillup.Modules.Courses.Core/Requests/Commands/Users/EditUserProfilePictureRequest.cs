using MediatR;
using Microsoft.AspNetCore.Http;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Users
{
    public record EditUserProfilePictureRequest(Guid UserId, IFormFile File) : IRequest;
}
