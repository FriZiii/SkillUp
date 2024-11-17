using MediatR;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Attachments
{
    internal class DeleteAttachmentHandler : IRequestHandler<DeleteAssetRequest>
    {
        public Task Handle(DeleteAssetRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
