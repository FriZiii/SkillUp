using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.User;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            var userMapper = new UserMapper();

            return userMapper.UserToUserDto(user, request.Details);
        }
    }
}
