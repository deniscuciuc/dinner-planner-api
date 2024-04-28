using System.Net;

namespace DinnerPlanner.Application.Common.Errors;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string? ErrorMessage { get; }
}