using DinnerPlanner.Application.Services.Authentication.Common;
using ErrorOr;

namespace DinnerPlanner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}