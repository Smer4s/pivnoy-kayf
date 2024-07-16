using BeerBuzz.Domain.Abstractions.Repositories;
using BeerBuzz.Domain.Entities;
using BeerBuzz.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeerBuzz.Infrastructure.Repositories;

public abstract class CrudRepository<T>(AppDbContext dbContext) : ICrudRepository<T> where T : Entity
{
    protected abstract IEnumerable<Expression<Func<T, object>>> Includes { get; }

    public void Create(T entity) => dbContext.Set<T>().Add(entity);

    public Task<int> ExecuteDelete(Guid id) => dbContext.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync();

    public Task<T?> Get(Guid id) => WithIncludes().FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<T>> GetAll() => WithIncludes().ToListAsync();

    public Task<List<T>> GetAll(IEnumerable<Guid> ids) => WithIncludes().Where(x => ids.Contains(x.Id)).ToListAsync();

    public Task<List<T>> GetAll(Expression<Func<T, bool>> predicate) => WithIncludes().Where(predicate).ToListAsync();

    public void Update(T entity) => dbContext.Set<T>().Update(entity);

    protected virtual IQueryable<T> WithIncludes()
    {
        var query = dbContext.Set<T>().AsQueryable();

        foreach (var include in Includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
