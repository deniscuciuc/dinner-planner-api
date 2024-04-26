﻿using DinnerPlanner.Application.Common.Interfaces.Authentication;

namespace DinnerPlanner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if already exists


        // Create user (generate guid)
        var userId = Guid.NewGuid();


        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(
            userId,
            firstName,
            lastName
        );


        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "First",
            "Last",
            email,
            "Token"
        );
    }
}