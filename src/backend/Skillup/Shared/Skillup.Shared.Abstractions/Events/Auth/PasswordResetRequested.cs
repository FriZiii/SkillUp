namespace Skillup.Shared.Abstractions.Events.Auth
{
    public record PasswordResetRequested(Guid UserId, string Token);
}
