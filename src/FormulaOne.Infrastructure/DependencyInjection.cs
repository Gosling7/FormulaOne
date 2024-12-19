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
                "Data Source=localhost,1433;" +
                "Initial Catalog=FormulaOne;" +
                "User ID=sa;Password=Password1!;" +
                "Encrypt=False;" +
                "Trust Server Certificate=True");
        });
        
        return services;
    }
}
