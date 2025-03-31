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

    public async Task<IServiceResult<T>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new ServiceResult<T>
            {
                Message = "Entity not found.",
                Data = null,
                Success = false,
            };
        return new ServiceResult<T>
        {
            Success = true,
            Message = "Entity found.",
            Data = entity,
        };
    }

    public async Task<IServiceResult> AddAsync(T entity)
    {
        if (entity is null)
            return new ServiceResult
            {
                Message = "Entity cannot be null.",
                Success = false,
            };
        
        await _repository.AddAsync(entity);
        return new ServiceResult
        {
            Message = "Entity added.",
            Success = true,
        };
    }

    public async Task<IServiceResult> UpdateAsync(T entity)
    {
        if (entity is null)
            return new ServiceResult
            {
                Message = "Entity cannot be null.",
                Success = false,
            };
        await _repository.UpdateAsync(entity);
        return new ServiceResult
        {
            Message = "Entity updated.",
            Success = true,
        };
    }

    public async Task<IServiceResult> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new ServiceResult
            {
                Message = "Entity not found.",
                Success = false,
            };
        await _repository.DeleteAsync(id);
        return new ServiceResult
        {
            Message = "Entity deleted.",
            Success = true,
        };
    }
}
