using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Attachments
{
    internal class DeleteAttachmentHandler(IElementAttachmentRepository elementAttachmentRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<DeleteAttachmentRequest>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task Handle(DeleteAttachmentRequest request, CancellationToken cancellationToken)
        {
            var attachment = await _elementAttachmentRepository.Get(request.AttachmentId) ?? throw new Exception(); // TODO: Custom ex

            await _amazonS3Service.Delete(S3FolderPaths.ElementsAttachments + attachment.Key);
            await _elementAttachmentRepository.Delete(attachment.Id);
        }
    }
}
