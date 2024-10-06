using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Services
{
    internal interface IAuthTokenStorage
    {
        void SetToken(Guid requestId, AuthTokens tokens);
        AuthTokens GetTokens(Guid requestId);
    }
}
