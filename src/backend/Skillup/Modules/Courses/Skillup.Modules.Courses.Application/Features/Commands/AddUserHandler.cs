using MediatR;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
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
            };

            await _userRepository.Add(user);
        }
    }
}
