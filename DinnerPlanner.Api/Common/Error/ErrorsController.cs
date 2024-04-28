using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Common.Error;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}