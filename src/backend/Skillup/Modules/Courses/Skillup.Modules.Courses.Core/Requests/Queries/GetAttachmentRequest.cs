using MediatR;
using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public class GetAttachmentRequest : IRequest<AttachmentDto>
    {
        public Guid AttachmentId { get; set; }
    }
}
