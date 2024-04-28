using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public class UserByEmailNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string? ErrorMessage => "User not found by given email";
}