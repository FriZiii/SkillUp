using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class AttachmentsMapper
    {
        public AttachmentDto AttachmentToDto(Attachment attachment)
        {
            return new AttachmentDto() { Id = attachment.Id, Name = attachment.Name, Type = attachment.Type, ElementId = attachment.ElementId };
        }
    }
}
