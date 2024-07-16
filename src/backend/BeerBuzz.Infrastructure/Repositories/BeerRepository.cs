using BeerBuzz.Domain.Abstractions.Repositories;
using BeerBuzz.Domain.Entities;
using BeerBuzz.Infrastructure.Database;
using System.Linq.Expressions;

namespace BeerBuzz.Infrastructure.Repositories;

public class BeerRepository(AppDbContext dbContext) : CrudRepository<Beer>(dbContext), IBeerRepository
{
    protected override IEnumerable<Expression<Func<Beer, object>>> Includes => [];
}
