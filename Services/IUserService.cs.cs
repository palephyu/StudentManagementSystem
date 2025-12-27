using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Services
{
    public interface IUserService
    {
       Task <IEnumerable<User_VM>> GetAll();
        Task<User_VM?> GetById(int id);
        Task  Create(User_VM vm);
        Task  Update(User_VM vm);
        Task  Delete(int id);
        UserTb Login(string username, int? password);


    }
}
