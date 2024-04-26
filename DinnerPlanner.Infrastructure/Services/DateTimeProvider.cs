using DinnerPlanner.Application.Common.Interfaces.Services;

namespace DinnerPlanner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}