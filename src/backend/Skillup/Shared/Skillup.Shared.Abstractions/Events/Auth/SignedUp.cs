namespace Skillup.Shared.Abstractions.Events.Auth
{
    public record SignedUp(Guid UserId, string Email, Guid ActivationToken, DateTime TokenExpiration);
}
