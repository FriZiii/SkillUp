using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment
{
    public record DeleteAttachmentRequest(Guid AttachmentId) : IRequest;
}
