using DinnerPlanner.Application.Common.Interfaces.Authentication;
using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Domain.Common.Errors;
using DinnerPlanner.Domain.Entities;
using ErrorOr;

namespace DinnerPlanner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Find if exists, if not return error
        var user = _userRepository.GetUserByEmail(email);
        if (user is null)
        {
            return Errors.Authentication.UserNotFound;
        }


        // Validate password
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidPassword;
        }


        // Create JWT token
        var token = GenerateToken(user);


        return new AuthenticationResult(user, token);
    }

    private string GenerateToken(User user)
    {
        return _jwtTokenGenerator.GenerateToken(user);
    }
}