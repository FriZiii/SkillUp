using MediatR;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAttachmentHandler : IRequestHandler<GetAttachmentRequest, AttachmentDto>
    {
        public Task<AttachmentDto> Handle(GetAttachmentRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
