﻿using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ISectionRepository
    {
        Task Add(Section section);
        Task<List<Section>> GetSectionsByCourseId(Guid courseId);
    }
}
