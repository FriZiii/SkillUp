using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Attachments
{
    internal class AddAttachmentHandler(IElementAttachmentRepository elementAttachmentRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<AddAttachmentRequest>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task Handle(AddAttachmentRequest request, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid();
            await _amazonS3Service.Upload(request.File, S3FolderPaths.ElementsAttachments + key);
            await _elementAttachmentRepository.Add(new Attachment()
            {
                Id = request.AttachmentId,
                ElementId = request.ElementId,
                Key = key,
            });
        }
    }
}
