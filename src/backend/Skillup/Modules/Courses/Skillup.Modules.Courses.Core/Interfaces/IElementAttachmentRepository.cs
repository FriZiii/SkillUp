﻿using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IElementAttachmentRepository
    {
        Task<Attachment?> Get(Guid id);
        Task Delete(Guid id);
        Task Add(Attachment attachment);
        Task<IEnumerable<Attachment>> GetByElementId(Guid elementId);
        Task<IEnumerable<Attachment>> GetByCourseId(Guid courseId);
    }
}
