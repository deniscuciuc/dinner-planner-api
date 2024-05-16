using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Dinner;

[Route("dinners")]
public class DinnersController : ApiControllerBase
{
    public DinnersController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }
}