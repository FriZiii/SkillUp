using MediatR;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Users;

namespace Skillup.Modules.Courses.Application.Features.Commands.Users
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
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Details = new UserDetails()
                {
                    Biography = request.Biography,
                    Title = request.Title,
                },
                SocialMediaLinks = request.SocialMediaLinks,
            };

            await _userRepository.Edit(user);
        }
    }
}
