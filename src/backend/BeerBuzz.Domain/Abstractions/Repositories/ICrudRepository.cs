using BeerBuzz.Domain.Entities;
using System.Linq.Expressions;

namespace BeerBuzz.Domain.Abstractions.Repositories;

public interface ICrudRepository<T> where T : Entity
{
    Task<T?> Get(Guid id);
    void Create(T entity);
    void Update(T entity);
    Task<int> ExecuteDelete(Guid id);
    Task<List<T>> GetAll();
    Task<List<T>> GetAll(IEnumerable<Guid> ids);
    Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
}
