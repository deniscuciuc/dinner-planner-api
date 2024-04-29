using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);