using DinnerPlanner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers;

[ApiController]
[Route("")]
public class ApiController : ControllerBase
{
    public IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}