using DinnerPlanner.Application.Common.Interfaces.Authentication;
using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Application.Services.Authentication.Common;
using DinnerPlanner.Domain.Common.Errors;
using DinnerPlanner.Domain.Entities;
using ErrorOr;

namespace DinnerPlanner.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email,
        string password)
    {
        // Check if already exists
        var userByEmail = _userRepository.GetUserByEmail(email);

        if (userByEmail is not null) return Errors.User.DuplicateEmail;


        // Create user and save in DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.AddUser(user);


        // Create JWT token
        var token = GenerateToken(user);


        return new AuthenticationResult(user, token);
    }

    private string GenerateToken(User user)
    {
        return _jwtTokenGenerator.GenerateToken(user);
    }
}