using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Dinner;

[Route("dinners")]
public class DinnersControllerBase : ApiControllerBase
{
    public DinnersControllerBase(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }
}