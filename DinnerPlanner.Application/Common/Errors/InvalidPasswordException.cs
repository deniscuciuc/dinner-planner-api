using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public class InvalidPasswordException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string? ErrorMessage => "Invalid password";
}