using BeerBuzz.Domain.Entities;
using Bogus;

namespace BeerBuzz.Infrastructure.Database.Seeds;

public abstract class BaseSeeder<TEntity>(AppDbContext dbContext)
where TEntity : class
{
    protected IEnumerable<Guid> GetIdsOf<T>() where T : Entity
    {
        return dbContext.Set<T>().Select(x => x.Id).AsEnumerable();
    }

    protected void Seed(
        int seedCount,
        Faker<TEntity> faker,
        Func<List<TEntity>, IEnumerable<TEntity>>? afterGenerateChanges = null)
    {
        var entitiesSet = dbContext.Set<TEntity>();

        if (entitiesSet.Any())
        {
            return;
        }

        var items = faker.Generate(seedCount);

        if (afterGenerateChanges is not null)
        {
            items = afterGenerateChanges(items).ToList();
        }

        entitiesSet.AddRange(items);
        dbContext.SaveChanges();
    }
}
