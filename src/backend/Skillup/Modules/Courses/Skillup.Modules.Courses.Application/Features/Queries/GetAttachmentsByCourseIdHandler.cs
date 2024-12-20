using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAttachmentsByCourseIdHandler(IElementAttachmentRepository elementAttachmentRepository) : IRequestHandler<GetAttachmentsByCourseIdRequest, IEnumerable<AttachmentDto>>
    {
        private readonly IElementAttachmentRepository _elementAttachmentRepository = elementAttachmentRepository;

        public async Task<IEnumerable<AttachmentDto>> Handle(GetAttachmentsByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new AttachmentsMapper();
            var attachments = await _elementAttachmentRepository.GetByCourseId(request.CourseId);
            return attachments.Select(mapper.AttachmentToDto);
        }
    }
}
