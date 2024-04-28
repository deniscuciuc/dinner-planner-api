using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public struct DuplicateEmailError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Given email is already in use";
}