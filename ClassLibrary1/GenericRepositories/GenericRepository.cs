using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestProject.Data.DbContexts;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Commons;

namespace TestProject.Data.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
    {
        private readonly TestProjectDbContext dbContext;
        private readonly DbSet<T> dbset;
        public GenericRepository(TestProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbset = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existEntity = await dbset.FirstOrDefaultAsync(e => e.Id.Equals(id));
            
            if(existEntity is not null)
            {
                dbset.Remove(existEntity);
                return true;
            }
            return false;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression is null)
                return dbset;
            
            return dbset.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => await GetAll().FirstOrDefaultAsync(expression);

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            return dbset.Update(entity).Entity;
        }
    }
}
