using HRSystem.Models;
using System.Linq.Expressions;

namespace HRSystem.Repositories.IRepositories
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1);
        Task<T> GetByIdAsync(int id,bool tracked = true, Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> AddAsync(T entity);
        Task SaveAsync();

    }
}
