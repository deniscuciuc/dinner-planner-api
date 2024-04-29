using DinnerPlanner.Application.Authentication.Commands.Register;
using DinnerPlanner.Application.Authentication.Queries.Login;
using DinnerPlanner.Application.Authentication.Results;
using DinnerPlanner.Contracts.Authentication.Requests;
using DinnerPlanner.Contracts.Authentication.Responses;
using DinnerPlanner.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var registerResult = await _sender.Send(command);

        return registerResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var loginResult = await _sender.Send(query);


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