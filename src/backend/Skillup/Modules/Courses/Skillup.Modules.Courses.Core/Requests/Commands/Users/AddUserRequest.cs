using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Users
{
    public record class AddUserRequest(Guid UserID, string Email) : IRequest;
}
