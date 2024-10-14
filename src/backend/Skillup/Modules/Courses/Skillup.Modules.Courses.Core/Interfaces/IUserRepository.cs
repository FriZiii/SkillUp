﻿using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Edit(User user);
        Task EditUserPrivacySettings(Guid userId, PrivacySettings privacySettings);
        Task<User?> GetById(Guid userId);
    }
}