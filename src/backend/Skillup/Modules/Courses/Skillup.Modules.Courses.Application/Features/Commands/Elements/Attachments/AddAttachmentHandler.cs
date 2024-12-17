using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Attachments
{
    internal class AddAttachmentHandler(IElementAttachmentRepository elementAttachmentRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<AddAttachmentRequest, AttachmentDto>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<AttachmentDto> Handle(AddAttachmentRequest request, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid();
            await _amazonS3Service.Upload(request.File, S3FolderPaths.ElementsAttachments + key);
            var fileName = request.File.FileName.Substring(0, request.File.FileName.LastIndexOf('.'));
            await _elementAttachmentRepository.Add(new Attachment()
            {
                Id = request.AttachmentId,
                Name = fileName,
                Type = Path.GetExtension(request.File.FileName),
                ElementId = request.ElementId,
                Key = key,
            });

            return new AttachmentDto
            {
                Id = request.AttachmentId,
                Name = fileName,
                Type = Path.GetExtension(request.File.FileName),
            };
        }
    }
}
