using BeerBuzz.Domain.Abstractions.Repositories;
using BeerBuzz.Domain.Models.Dto;
using MapsterMapper;
using MediatR;

namespace BeerBuzz.Domain.UseCases.Beer.GetAll;

public class GetAllBeerQueryHandler(IBeerRepository beerRepository, IMapper mapper) : IRequestHandler<GetAllBeerQuery, List<BeerDto>>
{
    public async Task<List<BeerDto>> Handle(GetAllBeerQuery request, CancellationToken cancellationToken)
    {
        var items = await beerRepository.GetAll();

        return items.Select(mapper.Map<BeerDto>).ToList();
    }
}
