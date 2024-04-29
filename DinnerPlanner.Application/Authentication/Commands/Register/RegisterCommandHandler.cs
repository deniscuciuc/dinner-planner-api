using DinnerPlanner.Application.Authentication.Results;
using DinnerPlanner.Application.Common.Interfaces.Authentication;
using DinnerPlanner.Application.Common.Interfaces.Persistence;
using DinnerPlanner.Domain.Common.Errors;
using DinnerPlanner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DinnerPlanner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        // Check if already exists
        var userByEmail = _userRepository.GetUserByEmail(command.Email);

        if (userByEmail is not null) return Errors.User.DuplicateEmail;


        // Create user and save in DB
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
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