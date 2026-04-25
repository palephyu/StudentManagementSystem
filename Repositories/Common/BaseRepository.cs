using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Repositories.Common;
using System.Linq.Expressions;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly StudentdbContext _context; // Database Connection
    private readonly DbSet<T> _dbSet; // Table ကို ကိုယ်စားပြု

    public BaseRepository(StudentdbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // အားလုံး ယူမယ်
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
        // AsNoTracking() = Performance ကောင်းအောင်၊ Read Only အတွက်
    }
    
    // ID နဲ့ တစ်ခုတည်း ယူမယ်
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    // အသစ် ထည့်မယ်
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        // သတိပြု: SaveChanges() ကို ဒီမှာ မခေါ်ရဘူး (UnitOfWork က ခေါ်မယ်)
    }

    // ရှိပြီးသား ကို ပြင်မယ်
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    // ဖျက်မယ်
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }
}
