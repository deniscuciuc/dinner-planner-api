using DinnerPlanner.Application.Authentication.Results;
using ErrorOr;
using MediatR;

namespace DinnerPlanner.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password)
    : IRequest<ErrorOr<AuthenticationResult>>;