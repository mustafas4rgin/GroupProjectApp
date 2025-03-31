using GroupApp.Core.Results;
using GroupApp.Data;

namespace GroupApp.Core.Concrete;

public interface IService<T> where T : class
{
    Task<IServiceResult<IEnumerable<T>>> GetAllAsync();
    Task<IServiceResult<T>> GetByIdAsync(int id);
    Task<IServiceResult> AddAsync(T entity);
    Task<IServiceResult> UpdateAsync(T entity);
    Task<IServiceResult> DeleteAsync(int id);
}
