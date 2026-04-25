using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {
       

        // READ (ဖတ်ခြင်း) - Database ကနေ Data ယူတယ်

        Task<T> GetByIdAsync(int id);                 // ID နဲ့ တစ်ခုတည်း ယူမယ်
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);



        // WRITE (ရေးသားခြင်း) - Database ကို Data ပြောင်းလဲတယ်
        Task CreateAsync(T entity);    // အသစ် ထည့်မယ်
        void Update(T entity);          // ရှိပြီးသား ကို ပြင်မယ်
        void Delete(T entity);          // ဖျက်မယ်

        // UTILITY
        Task<bool> ExistsAsync(int id); // ရှိမရှိ စစ်မယ်
    }

}

