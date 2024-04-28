using FluentResults;

namespace DinnerPlanner.Application.Services.Authentication;

public interface IAuthenticationService
{
    Result<AuthenticationResult> Register(string firstName, string lastName, string email,
        string password);

    Result<AuthenticationResult> Login(string email, string password);
}