using FormulaOne.Application.Helpers;
using FormulaOne.Application.Interfaces;
using FormulaOne.Application.Services;
using FormulaOne.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FormulaOne.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services
            .AddScoped<ITeamService, TeamService>()
            .AddScoped<IDriverService, DriverService>()
            .AddScoped<ICircuitService, CircuitService>()
            .AddTransient<IParameterValidatorHelper, ParameterValidatorHelper>()
            .AddTransient<IQueryTeamParameterValidator, QueryTeamParameterValidator>()
            .AddTransient<IQueryDriverParameterValidator, QueryDriverParameterValidator>()
            .AddTransient<IQueryCircuitParameterValidator, QueryCircuitParameterValidator>()
            .AddTransient<PagedQueryHelper>();

        return services;
    }
}
