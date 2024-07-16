namespace BeerBuzz.Domain.Entities;

public class Beer : Entity
{
    public string Name { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;
}
