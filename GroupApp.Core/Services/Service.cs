using GroupApp.Core.Concrete;
using GroupApp.Core.Results;

public class Service<T> : IService<T> where T : class
{
    private readonly IRepository<T> _repository;

    public Service(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IServiceResult<IEnumerable<T>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        if (!entities.Any() || entities is null)
            return new ServiceResult<IEnumerable<T>>
            {
                Message = "There is no entity.",
                Data = new List<T>(),
                Success = false,
            };

        return new ServiceResult<IEnumerable<T>>
        {
            Data = entities,
            Message = "Entities:",
            Success = true,
        };
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
