﻿using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAttachmentHandler(IElementAttachmentRepository elementAttachmentRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetAttachmentRequest, AttachmentDto>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<AttachmentDto> Handle(GetAttachmentRequest request, CancellationToken cancellationToken)
        {
            var attachment = await _elementAttachmentRepository.Get(request.AttachmentId) ?? throw new Exception(); // TODO: Custom ex

            var response = await _amazonS3Service.Download(S3FolderPaths.ElementsAttachments + attachment.Key) ?? throw new Exception(); // TODO: Custom ex;

            using var responseStream = response.ResponseStream;

            var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            return new AttachmentDto()
            {
                Id = attachment.Id,
                ContentType = response.Headers.ContentType,
                FileData = memoryStream.ToArray(),
                FileName = response.Key,
            };
        }
    }
}
