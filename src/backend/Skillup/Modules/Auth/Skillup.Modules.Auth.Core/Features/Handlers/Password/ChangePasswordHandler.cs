using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Features.Requests.Password;
using Skillup.Modules.Auth.Core.Repositories;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Password
{
    internal class ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : IRequestHandler<ChangePasswordRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            //TODO : ChangePasswordHandler

            //var user = await _userRepository.Get(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            //
            //if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.CurrentPassword) ==
            //    PasswordVerificationResult.Failed)
            //{
            //    throw new InvalidPasswordException("current password is invalid");
            //}
            //
            //user.Password = _passwordHasher.HashPassword(user, request.NewPassword); ;
            //await _userRepository.Update(user);

            //TODO : LOGS
        }
    }
}
