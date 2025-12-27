using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Repositories.Domain
{
    public interface IUserRepository : IBaseRepository<UserTb>
    {
        public IQueryable<UserTb> GetAll();
        


    }
}
