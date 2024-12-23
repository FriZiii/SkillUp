using MediatR;

namespace Skillup.Shared.Abstractions.Events.Auth
{
    public record PasswordChanged(Guid UserId) : IRequest;
}
