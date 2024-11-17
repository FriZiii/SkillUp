using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment
{
    internal class DeleteAttachmentRequest : IRequest
    {
        public Guid AttachmentId { get; set; }
    }
}
