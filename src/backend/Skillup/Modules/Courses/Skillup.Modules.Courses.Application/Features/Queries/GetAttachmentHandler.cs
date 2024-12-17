using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Skillup.Shared.Abstractions.S3;
using System.Text;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAttachmentHandler(IElementAttachmentRepository elementAttachmentRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetAttachmentRequest, AttachmentFileDto>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<AttachmentFileDto> Handle(GetAttachmentRequest request, CancellationToken cancellationToken)
        {
            var attachment = await _elementAttachmentRepository.Get(request.AttachmentId) ?? throw new Exception(); // TODO: Custom ex: attachment with id doesnt exist

            var response = await _amazonS3Service.Download(S3FolderPaths.ElementsAttachments + attachment.Key) ?? throw new Exception(); // TODO: Custom ex: attachment file with key doesnt exist

            using var responseStream = response.ResponseStream;

            var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            return new AttachmentFileDto()
            {
                Id = attachment.Id,
                ContentType = response.Headers.ContentType,
                FileData = memoryStream.ToArray(),
                FileName = Encoding.UTF8.GetString(Convert.FromBase64String(response.Metadata["x-amz-meta-orginalname"])),
            };
        }
    }
}
