using DinnerPlanner.Application.Common.Errors.Authentication;
using DinnerPlanner.Application.Services.Authentication;
using DinnerPlanner.Contracts.Authentication.Requests;
using DinnerPlanner.Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Authentication;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var registerResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        if (registerResult.IsSuccess) return Ok(MapAuthResultToResponse(registerResult.Value));

        var errors = registerResult.Errors;

        if (errors[0] is DuplicateEmailError)
            return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already in use");

        return Problem();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _authenticationService.Login(request.Email, request.Password);

        if (loginResult.IsSuccess) return Ok(MapAuthResultToResponse(loginResult.Value));

        var errors = loginResult.Errors;

        if (errors[0] is UserByEmailNotFoundError)
            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "User with given email not found");

        if (errors[1] is InvalidPasswordError)
            return Problem(statusCode: StatusCodes.Status403Forbidden, detail: "Given password is incorrect");

        return Problem();
    }

    private AuthenticationResponse MapAuthResultToResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            FirstName: authResult.User.FirstName,
            LastName: authResult.User.LastName,
            Email: authResult.User.Email,
            Id: authResult.User.Id,
            Token: authResult.Token
        );
    }
}