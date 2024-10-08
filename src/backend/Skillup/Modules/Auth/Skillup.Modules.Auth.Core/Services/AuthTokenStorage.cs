using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Storage;

namespace Skillup.Modules.Auth.Core.Services
{
    internal class AuthTokenStorage : IAuthTokenStorage
    {
        private readonly IMemoryStorage _memoryStorage;

        public AuthTokenStorage(IMemoryStorage memoryStorage)
        {
            _memoryStorage = memoryStorage;
        }

        public AuthTokens GetTokens(Guid requestId)
        {
            return _memoryStorage.Get<AuthTokens>(GetKey(requestId));
        }

        public void SetToken(Guid requestId, AuthTokens tokens)
        {
            _memoryStorage.Set(GetKey(requestId), tokens);
        }

        private static string GetKey(Guid commandId) => $"jwt:{commandId:N}";
    }
}
