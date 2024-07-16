using BeerBuzz.Domain.Entities;
using Bogus;

namespace BeerBuzz.Infrastructure.Database.Seeds;

public class BeerSeeder(AppDbContext dbContext) : BaseSeeder<Beer>(dbContext), ISeeder
{
    public void Seed()
    {
        const int count = 15;

        var faker = new Faker<Beer>()
            .Rules((f, b) =>
            {
                b.Name = f.Random.Word();
                b.PhotoUrl = f.Random.Word();
            });

        Seed(count, faker, items => items.DistinctBy(x => x.Name));
    }
}
