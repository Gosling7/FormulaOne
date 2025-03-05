using FormulaOne.Application.Interfaces;
using FormulaOne.Infrastructure.Repositories;
using FormulaOne.Infrastructure.Scrapers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormulaOne.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var dbHost = configuration["MSSQL_HOST"];
            var dbPassword = configuration["MSSQL_SA_PASSWORD"];
            var dbName = configuration["MSSQL_DB_NAME"];

            services.AddDbContext<FormulaOneDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                    $"Data Source={dbHost},1433;" +
                    $"Initial Catalog={dbName};" +
                    "User ID=sa;" +
                    $"Password={dbPassword};" +
                    "Encrypt=False;" +
                    "Trust Server Certificate=True");
            });
        }
        else
        {
            services.AddDbContext<FormulaOneDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });
        }

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