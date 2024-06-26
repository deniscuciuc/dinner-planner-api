﻿using DinnerPlanner.Application.Authentication.Commands.Register;
using DinnerPlanner.Application.Authentication.Queries.Login;
using DinnerPlanner.Contracts.Authentication.Requests;
using DinnerPlanner.Contracts.Authentication.Responses;
using DinnerPlanner.Domain.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Api.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController : ApiControllerBase
{
    public AuthenticationController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = Mapper.Map<RegisterCommand>(request);

        var registerResult = await Sender.Send(command);

        return registerResult.Match(
            authResult => Ok(Mapper.Map<AuthenticationResponse>(authResult)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = Mapper.Map<LoginQuery>(request);
        var loginResult = await Sender.Send(query);


        if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidPassword)
            // TODO: fix in future (no code of error)
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: loginResult.FirstError.Description
            );

        return loginResult.Match(
            authResult => Ok(Mapper.Map<AuthenticationResponse>(authResult)),
            Problem
        );
    }
}