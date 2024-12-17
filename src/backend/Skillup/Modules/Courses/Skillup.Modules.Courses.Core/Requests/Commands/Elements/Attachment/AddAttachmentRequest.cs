using MediatR;
using Microsoft.AspNetCore.Http;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment
{
    public record AddAttachmentRequest : IRequest<AttachmentDto>
    {
        public AddAttachmentRequest(IFormFile file, Guid elementId)
        {
            File = file;
            ElementId = elementId;
        }

        public IFormFile File { get; set; }
        public Guid ElementId { get; set; }
        public Guid AttachmentId { get; set; } = Guid.NewGuid();
    }
}
