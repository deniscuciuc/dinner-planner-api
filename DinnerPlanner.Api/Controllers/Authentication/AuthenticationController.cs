using DinnerPlanner.Application.Services.Authentication;
using DinnerPlanner.Contracts.Authentication.Requests;
using DinnerPlanner.Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Authentication;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
            FirstName: authResult.FirstName,
            LastName: authResult.LastName,
            Email: authResult.Email,
            Id: authResult.Id,
            Token: authResult.Token
        );

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(request);
    }
}