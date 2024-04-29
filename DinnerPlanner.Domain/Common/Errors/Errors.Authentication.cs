using ErrorOr;

namespace DinnerPlanner.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidPassword => Error.Validation("User.InvalidPassword",
            "Invalid password for given email");

        public static Error UserNotFound => Error.NotFound("User.NotFound", "User not found");
    }
}