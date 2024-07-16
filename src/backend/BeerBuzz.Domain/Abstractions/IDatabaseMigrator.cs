namespace BeerBuzz.Domain.Abstractions;

public interface IDatabaseMigrator
{
    void Migrate();

    void ApplySeeds();
}
