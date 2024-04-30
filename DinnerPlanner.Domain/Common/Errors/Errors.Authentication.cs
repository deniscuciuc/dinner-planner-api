using ErrorOr;

namespace DinnerPlanner.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidPassword => Error.Validation("User.InvalidPassword",
            "Invalid password for given email");

        public static Error UserNotFoundByEmail =>
            Error.NotFound("User.NotFoundByEmail", "User with given email not found");
    }
}