using DinnerPlanner.Domain.Entities;

namespace DinnerPlanner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}