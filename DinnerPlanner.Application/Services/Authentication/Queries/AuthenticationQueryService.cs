using DinnerPlanner.Application.Common.Interfaces.Authentication;
using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Application.Services.Authentication.Common;
using DinnerPlanner.Domain.Common.Errors;
using DinnerPlanner.Domain.Entities;
using ErrorOr;

namespace DinnerPlanner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Find if exists, if not return error
        var user = _userRepository.GetUserByEmail(email);
        if (user is null) return Errors.Authentication.UserNotFound;


        // Validate password
        if (user.Password != password) return Errors.Authentication.InvalidPassword;


        // Create JWT token
        var token = GenerateToken(user);


        return new AuthenticationResult(user, token);
    }

    private string GenerateToken(User user)
    {
        return _jwtTokenGenerator.GenerateToken(user);
    }
}