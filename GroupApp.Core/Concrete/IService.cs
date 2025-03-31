using GroupApp.Core.Results;

namespace GroupApp.Core.Concrete;

public interface IService<T> where T : class
{
    Task<IServiceResult<IEnumerable<T>>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
