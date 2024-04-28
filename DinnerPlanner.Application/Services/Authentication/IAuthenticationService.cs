using DinnerPlanner.Application.Common.Errors;
using OneOf;

namespace DinnerPlanner.Application.Services.Authentication;

public interface IAuthenticationService
{
    OneOf<AuthenticationResult, DuplicateEmailError> Register(string firstName, string lastName, string email,
        string password);

    OneOf<AuthenticationResult, InvalidPasswordError> Login(string email, string password);
}