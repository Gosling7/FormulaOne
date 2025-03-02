using FormulaOne.Application.Interfaces;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Infrastructure.Scrapers;
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
            .AddScoped<ITeamRepository, TeamRepository>()
            .AddScoped<ITeamStandingRepository, TeamStandingRepository>()
            .AddScoped<IDriverRepository, DriverRepository>()
            .AddScoped<IDriverStandingRepository, DriverStandingRepository>()
            .AddScoped<IRaceResultRepository, RaceResultRepository>()
            .AddScoped<ICircuitRepository, CircuitRepository>()

            .AddScoped<IDatabaseSeeder, DatabaseSeeder>()

            .AddScoped<CircuitScraper>()
            .AddScoped<DriverScraper>()
            .AddScoped<DriverStandingScraper>()
            .AddScoped<RaceResultLinksScraper>()
            .AddScoped<RaceResultScraper>()
            .AddScoped<TeamScraper>()
            .AddScoped<TeamStandingScraper>();

        return services;
    }
}