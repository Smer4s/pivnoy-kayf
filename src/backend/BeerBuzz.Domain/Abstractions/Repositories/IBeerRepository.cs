using BeerBuzz.Domain.Entities;

namespace BeerBuzz.Domain.Abstractions.Repositories;

public interface IBeerRepository : ICrudRepository<Beer>
{ }
