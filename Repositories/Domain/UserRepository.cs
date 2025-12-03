using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Domain
{
    public class UserRepository : BaseRepository<UserTb>, IUserRepository
    {
        private readonly StudentdbContext _studentdbContext;
        public UserRepository(StudentdbContext context) : base(context)
        {
            _studentdbContext = context;

        }

        public Task<UserTb> CreateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
