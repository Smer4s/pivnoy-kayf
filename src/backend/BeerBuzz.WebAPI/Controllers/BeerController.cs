using BeerBuzz.Domain.UseCases.Beer.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeerBuzz.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BeerController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBeers()
    {
        var items = await sender.Send(new GetAllBeerQuery());

        return Ok(items);
    }
}
