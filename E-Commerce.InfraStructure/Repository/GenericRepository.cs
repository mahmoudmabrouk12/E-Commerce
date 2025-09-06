using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace E_Commerce.InfraStructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T Entity)
        {
            await context.Set<T>().AddAsync(Entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
             var Entity = await context.Set<T>().FindAsync(Id);
             context.Set<T>().Remove(Entity);
             await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
      =>await context.Set<T>().AsNoTracking().ToListAsync();
        

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] Includes)
        {  
            var Query = context.Set<T>().AsQueryable();
            foreach (var item in Includes)
            {
                Query = Query.Include(item);
            }
          return  await Query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            var Entity = await context.Set<T>().FindAsync(Id);
            return Entity;
        }

        public async Task<T> GetByIdAsync(int Id, params Expression<Func<T, object>>[] Includes)
        {
            IQueryable<T>? Query = context.Set<T>().AsQueryable();
            foreach (var item in Includes)
            {
                Query = Query.Include(item);
            }
            var Entity = Query.FirstOrDefault(l => EF.Property<int>(l, "Id") == Id);
            return Entity;
        }

        public async Task UpdateAsync(T Entity)
        {
             context.Entry(Entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
