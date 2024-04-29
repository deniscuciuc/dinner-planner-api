using DinnerPlanner.Application.Services.Authentication;
using DinnerPlanner.Contracts.Authentication.Requests;
using DinnerPlanner.Contracts.Authentication.Responses;
using DinnerPlanner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController : ApiController
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
            Problem
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _authenticationService.Login(request.Email, request.Password);

        if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidPassword)
            // TODO: fix in future (no code of error)
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: loginResult.FirstError.Description
            );

        return loginResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            Problem
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