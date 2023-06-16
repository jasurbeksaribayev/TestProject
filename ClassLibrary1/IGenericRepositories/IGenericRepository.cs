using System.Linq.Expressions;
using TestProject.Domain.Commons;

namespace TestProject.Data.IGenericRepositories
{
    public interface IGenericRepository<T> where T : Auditable
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
