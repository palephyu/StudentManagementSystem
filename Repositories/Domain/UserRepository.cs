using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Repositories.Domain
{
    public class UserRepository : BaseRepository<UserTb>, IUserRepository
    {
        private readonly StudentdbContext _studentdbContext;
        public UserRepository(StudentdbContext context) : base(context)
        {
            _studentdbContext = context;

        }
        public IQueryable<UserTb> GetAll()
        {
            return _studentdbContext.UserTbs.AsQueryable();
        }



    }
}
