using FormulaOne.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Tests.Integration.Repositories;

public class RepositoryTestsBase
{
    protected readonly FormulaOneDbContext _dbContext;

    public RepositoryTestsBase()
    {
        var options = new DbContextOptionsBuilder<FormulaOneDbContext>()
            .UseSqlServer(SqlServerDbSingleton.Instance.GetConnectionString())
            .Options;
        _dbContext = new FormulaOneDbContext(options);

        DatabaseSeeder.EnsureSeeded(_dbContext);
    }
}
