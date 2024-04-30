using DinnerPlanner.Application.Authentication.Results;
using ErrorOr;
using MediatR;

namespace DinnerPlanner.Application.Authentication.Commands.Register;

/// <summary>
///     This command registers new user in database.
///     The command handler : <see cref="RegisterCommandHandler" />
/// </summary>
/// <param name="FirstName">user's first name</param>
/// <param name="LastName">user's last name</param>
/// <param name="Email">user's email</param>
/// <param name="Password">user's password</param>
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;