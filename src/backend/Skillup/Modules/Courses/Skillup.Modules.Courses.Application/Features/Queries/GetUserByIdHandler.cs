using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.User;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAmazonS3Service _s3Service;

        public GetUserByIdHandler(IUserRepository userRepository, IAmazonS3Service s3Service)
        {
            _userRepository = userRepository;
            _s3Service = s3Service;
        }

        public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            var userMapper = new UserMapper(_s3Service);

            return await userMapper.UserToUserDto(user, request.Details);
        }
    }
}
