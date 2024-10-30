using MediatR;
using Skillup.Modules.Mails.Core.Entities;
using Skillup.Modules.Mails.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class EditUserHandler : IRequestHandler<EditUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public EditUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = request.UserId,
                Email = request.Email,
                AllowMarketingEmails = request.AllowMarketingEmails
            };

            await _userRepository.Update(user);
        }
    }
}
