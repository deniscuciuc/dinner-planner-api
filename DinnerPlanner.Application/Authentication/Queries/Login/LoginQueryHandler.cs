using DinnerPlanner.Application.Authentication.Results;
using DinnerPlanner.Application.Common.Interfaces.Authentication;
using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Domain.Common.Errors;
using DinnerPlanner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DinnerPlanner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Find if exists, if not return error    
        var user = _userRepository.GetUserByEmail(query.Email);
        if (user is null) return Errors.Authentication.UserNotFoundByEmail;


        // Validate password
        if (user.Password != query.Password) return Errors.Authentication.InvalidPassword;


        // Create JWT token
        var token = GenerateToken(user);


        return new AuthenticationResult(user, token);
    }

    private string GenerateToken(User user)
    {
        return _jwtTokenGenerator.GenerateToken(user);
    }
}