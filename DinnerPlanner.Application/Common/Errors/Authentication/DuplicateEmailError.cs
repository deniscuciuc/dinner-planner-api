using FluentResults;

namespace DinnerPlanner.Application.Common.Errors.Authentication;

public class DuplicateEmailError : IError
{
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }
    public List<IError> Reasons { get; }
}