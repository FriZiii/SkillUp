using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCommentsByElementIdHandler(ICommentRepository commentRepository, IAmazonS3Service amazonS3Service, ILogger<GetCommentsByElementIdHandler> logger) : IRequestHandler<GetCommentsByElementIdRequest, IEnumerable<CommentDto>>
    {
        private readonly ICommentRepository commentRepository = commentRepository;
        private readonly IAmazonS3Service amazonS3Service = amazonS3Service;
        private readonly ILogger<GetCommentsByElementIdHandler> logger = logger;

        public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByElementIdRequest request, CancellationToken cancellationToken)
        {
            var comments = await commentRepository.GetByElementId(request.ElementId);
            return comments.Select(x => CommentsMapper.CommentToDto(x, request.UserId, amazonS3Service, logger));
        }
    }
}
