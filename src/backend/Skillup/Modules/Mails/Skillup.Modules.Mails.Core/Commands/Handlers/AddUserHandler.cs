using MediatR;
using Skillup.Modules.Mails.Core.Entities;
using Skillup.Modules.Mails.Core.Repositories;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class AddUserHandler : IRequestHandler<AddUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = request.UserID,
                Email = request.Email,
                AllowMarketingEmails = request.AllowMarketingEmails,
            };

            await _userRepository.Add(user);
        }
    }
}
