using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Application.Authentication.Results;

public record AuthenticationResult(
    User User,
    string Token);