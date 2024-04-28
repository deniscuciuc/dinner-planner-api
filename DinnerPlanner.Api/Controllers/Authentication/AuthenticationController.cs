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

        return registerResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            errorResult => Problem(errorResult.ErrorMessage, statusCode: (int)errorResult.StatusCode)
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _authenticationService.Login(request.Email, request.Password);

        return loginResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            errorResult => Problem(errorResult.ErrorMessage, statusCode: (int)errorResult.StatusCode)
        );
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