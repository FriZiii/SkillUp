using MediatR;

namespace Skillup.Modules.Courses.Core.Requests
{
    public record class AddUserRequest(Guid UserID, string Email) : IRequest;
}
