using Skillup.Modules.Mails.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.Repositories
{
    internal interface IUserRepository
    {
        Task Add(User user);
        Task<User?> Get(Guid id);
        Task Update(User user);
    }
}
