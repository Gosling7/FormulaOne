using FormulaOne.Core.Entities;
using FormulaOne.Infrastructure;
using FormulaOne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace FormulaOne.Tests.Integration;

public class DbContainerFixture : IAsyncLifetime
{
    public CircuitRepository CircuitRepository { get; private set; } = null!;

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU17-ubuntu-22.04")
        .WithEnvironment("MSSQL_SA_PASSWORD", "yourStrong(!)Password")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithPortBinding(1433, true)
        .Build();

    private FormulaOneDbContext _dbContext = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var options = new DbContextOptionsBuilder<FormulaOneDbContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;

        _dbContext = new FormulaOneDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();

        CircuitRepository = new CircuitRepository(_dbContext);

        await _dbContext.Circuits.AddRangeAsync(
            new Circuit(
                id: Guid.Parse(TestConstant.ValidId1),
                name: TestConstant.CircuitName1,
                location: TestConstant.CircuitLocation1),
            new Circuit(
                id: Guid.Parse(TestConstant.ValidId2),
                name: TestConstant.CircuitName2,
                location: TestConstant.CircuitLocation2));

        await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
