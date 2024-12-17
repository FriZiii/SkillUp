using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAttachmentsByElementIdHandler(IElementAttachmentRepository elementAttachmentRepository) : IRequestHandler<GetAttachmentsByElementIdRequest, IEnumerable<AttachmentDto>>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;

        public async Task<IEnumerable<AttachmentDto>> Handle(GetAttachmentsByElementIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new AttachmentsMapper();
            var attachments = await _elementAttachmentRepository.GetByElementId(request.ElementId);
            return attachments.Select(mapper.AttachmentToDto);
        }
    }
}
