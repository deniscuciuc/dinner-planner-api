using FluentResults;

namespace DinnerPlanner.Application.Common.Errors.Authentication;

public class InvalidPasswordError : IError
{
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }
    public List<IError> Reasons { get; }
}