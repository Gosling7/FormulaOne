using Testcontainers.MsSql;

namespace FormulaOne.Tests.Integration;

public class SqlServerDbSingleton : IAsyncDisposable
{
    private static readonly Lazy<Task<SqlServerDbSingleton>> _lazy = new(async () =>
    {
        var singleton = new SqlServerDbSingleton();
        await singleton.InitializeAsync();
        return singleton;
    });

    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU17-ubuntu-22.04")
        .WithEnvironment("MSSQL_SA_PASSWORD", "yourStrong(!)Password")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithPortBinding(1433, true)
        .Build();

    public static SqlServerDbSingleton Instance => _lazy.Value.Result;

    public string GetConnectionString() => _container.GetConnectionString();

    private async Task InitializeAsync() => await _container.StartAsync();

    public async ValueTask DisposeAsync() => await _container.DisposeAsync();
}
