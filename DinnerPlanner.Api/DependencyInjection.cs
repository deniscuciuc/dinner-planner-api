using DinnerPlanner.Api.Common.Errors;
using DinnerPlanner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DinnerPlanner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, DinnerPlannerProblemDetailsFactory>();
        services.AddControllers();
        services.AddMapping();
        return services;
    }
}