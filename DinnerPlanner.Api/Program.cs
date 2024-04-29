using DinnerPlanner.Api.Common.Errors;
using DinnerPlanner.Application;
using DinnerPlanner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddSingleton<ProblemDetailsFactory, DinnerPlannerProblemDetailsFactory>();

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}