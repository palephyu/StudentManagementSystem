using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task Create(User_VM vm)
        {
            var users = new UserTb
            {
                UserPkid = vm.UserPkid,
                Username = vm.Username,
                UserType = vm.UserType,
                Password = vm.Password,
                Role = vm.Role
            };

            await _uow.UserRepository.CreateAsync(users);
            await _uow.Commit();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User_VM>> GetAll()
        {
            var teachers = await _uow.UserRepository.GetAllAsync();
            var list = teachers.Select(e => new User_VM
            {
                UserPkid = e.UserPkid,
                Username = e.Username,
                UserType = e.UserType,
                Password = e.Password,
                Role =  e.Role
            });
            return list;
        }

        public Task<User_VM?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(User_VM vm)
        {
            throw new NotImplementedException();
        }

       // ✅ Login Method (IMPORTANT)
        // Fix: Convert password string to int before comparison, and handle nulls safely.
        public UserTb? Login(string username, int? password)
        {
           
            return _uow.UserRepository
                       .GetAll()
                       .FirstOrDefault(x =>
                            x.Username == username &&
                            x.Password == password);
        }
    }
}
