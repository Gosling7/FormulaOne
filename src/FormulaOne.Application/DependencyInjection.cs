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
            .AddTransient<ITeamService, TeamService>()
            .AddTransient<IDriverService, DriverService>()
            .AddTransient<IParameterValidatorHelper, ParameterValidatorHelper>()
            .AddTransient<IQueryTeamParameterValidator, QueryTeamsParameterValidator>()
            .AddTransient<IQueryDriverParameterValidator, QueryDriverParameterValidator>()
            .AddTransient<ServiceHelper>();

        return services;
    }
}
