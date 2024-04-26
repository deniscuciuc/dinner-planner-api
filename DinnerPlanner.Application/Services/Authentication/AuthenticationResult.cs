using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);