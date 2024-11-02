using MediatR;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class GetUserHandler : IRequestHandler<GetUserRequest, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                AllowMarketingEmails = user.AllowMarketingEmails,
            };

            return userDto;
        }
    }
}
