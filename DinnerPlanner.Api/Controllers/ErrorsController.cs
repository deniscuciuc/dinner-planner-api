using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}