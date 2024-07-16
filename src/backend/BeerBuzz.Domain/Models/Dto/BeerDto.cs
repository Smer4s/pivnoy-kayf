using BeerBuzz.Domain.Abstractions;
using BeerBuzz.Domain.Entities;
using Mapster;

namespace BeerBuzz.Domain.Models.Dto;

public record BeerDto : IMapFrom<Beer>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Beer, BeerDto>()
            .RequireDestinationMemberSource(true);
    }
}
