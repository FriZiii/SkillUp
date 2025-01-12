using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetUserByIdHandler> _logger;

        public GetUserByIdHandler(IUserRepository userRepository, IAmazonS3Service s3Service, ILogger<GetUserByIdHandler> logger)
        {
            _userRepository = userRepository;
            _s3Service = s3Service;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException($"User with ID {request.UserId} not found");
            var userMapper = new UserMapper(_s3Service, _logger);

            return userMapper.UserToUserDto(user, request.Details);
        }
    }
}
