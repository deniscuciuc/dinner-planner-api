using DinnerPlanner.Api.Common.Http;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace DinnerPlanner.Api.Controllers;

[ApiController]
[Route("")]
public abstract class ApiController : ControllerBase
{
    protected readonly IMapper Mapper;
    protected readonly ISender Sender;

    protected ApiController(ISender sender, IMapper mapper)
    {
        Sender = sender;
        Mapper = mapper;
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.IsNullOrEmpty()) return Problem();

        if (errors.All(error => error.Type == ErrorType.Validation)) return ValidationProblem(errors);

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors.First();

        return Problem(firstError);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors) modelStateDictionary.AddModelError(error.Code, error.Description);

        return ValidationProblem(modelStateDictionary);
    }
}