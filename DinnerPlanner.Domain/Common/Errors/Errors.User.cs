using ErrorOr;

namespace DinnerPlanner.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail = Error.Conflict("User.DuplicateEmail",
            "User with given email already exists");
    }
}