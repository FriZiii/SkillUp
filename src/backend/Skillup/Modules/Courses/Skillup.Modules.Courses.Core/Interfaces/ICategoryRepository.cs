﻿using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task Add(Category category);
        Task<IEnumerable<Category>> GetAll();
    }
}
