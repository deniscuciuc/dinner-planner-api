using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public struct UserByEmailNotFoundError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "User not found by given email";
}