using BeerBuzz.Domain.Models.Dto;
using MediatR;

namespace BeerBuzz.Domain.UseCases.Beer.GetAll;

public record GetAllBeerQuery : IRequest<List<BeerDto>>
{ }
