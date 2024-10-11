﻿using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task Add(Course course);
        Task<IEnumerable<Course>> GetAll();
        Task EditDetails(Guid courseId, CourseDetails details);
        Task<Course?> GetById(Guid id);
    }
}
