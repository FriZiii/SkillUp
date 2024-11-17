using MediatR;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Attachments
{
    internal class AddAttachmentHandler : IRequestHandler<AddAttachmentRequest>
    {
        public Task Handle(AddAttachmentRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
