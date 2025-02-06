using FormulaOne.Application.Interfaces;
using FormulaOne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FormulaOne.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext<FormulaOneDbContext>(optionsBuilder =>
        {            
            optionsBuilder.UseSqlServer(
                "Data Source=mssql,1433;" + // Container name in docker-compose
                //"Data Source=localhost,1433;" + // For ef core tools 
                "Initial Catalog=FormulaOne;" +
                "User ID=sa;Password=Password1!;" +
                "Encrypt=False;" +
                "Trust Server Certificate=True");
        });

        services
            .AddTransient<ITeamRepository, TeamRepository>()
            .AddTransient<ITeamStandingRepository, TeamStandingRepository>()
            .AddTransient<IDriverRepository, DriverRepository>()
            .AddTransient<IDriverStandingRepository, DriverStandingRepository>()
            .AddTransient<IRaceResultRepository, RaceResultRepository>()
            .AddTransient<ICircuitRepository, CircuitRepository>();

        return services;
    }
}
