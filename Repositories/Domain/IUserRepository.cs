using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;

namespace StudentManagementSystem.Repositories.Domain
{
    public interface IUserRepository : IBaseRepository<UserTb>
    {
        Task<UserTb> CreateAsync();

    }
}
