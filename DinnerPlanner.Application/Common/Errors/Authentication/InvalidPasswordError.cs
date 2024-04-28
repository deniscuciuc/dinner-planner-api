using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public struct InvalidPasswordError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public string ErrorMessage => "Invalid password";
}