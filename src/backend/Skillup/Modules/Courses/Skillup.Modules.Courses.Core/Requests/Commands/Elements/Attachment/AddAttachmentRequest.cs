using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment
{
    public class AddAttachmentRequest : IRequest
    {
        public Guid ElementId { get; set; }
    }
}
