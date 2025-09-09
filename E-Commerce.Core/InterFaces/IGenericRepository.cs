using System.Linq.Expressions;

namespace E_Commerce.Core.InterFaces
{
    public interface IGenericRepository<T> where T : class
    {
        public  Task<IReadOnlyList<T>> GetAllAsync();
        public  Task AddAsync(T Entity);
        public  Task UpdateAsync(T Entity);
        public  Task DeleteAsync(int Id);
        public  Task<T> GetByIdAsync(int Id);


        public Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T,object >>[] Includes );
        public Task<T> GetByIdAsync(int Id, params Expression<Func<T, object>>[] Includes);

        public Task<int> CountAsync();


    }
}
