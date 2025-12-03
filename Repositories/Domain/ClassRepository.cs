using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories.Common;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Repositories.Domain
{
    public class ClassRepository : BaseRepository<ClassTb>, IClassRepository
    {
        private readonly StudentdbContext _context;
        public ClassRepository(StudentdbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassTb>> GetAllClassesAsync()
            => await _context.ClassTbs.AsNoTracking().ToListAsync();
    
    }
}
